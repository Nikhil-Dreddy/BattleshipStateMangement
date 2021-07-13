using System;
using System.Collections.Generic;
using System.Linq;
using BattleshipStateMangement;
using BattleshipStateMangement.Exceptions;
using BattleshipStateMangement.Interfaces;
using BattleshipStateMangement.Models;
using FakeItEasy;
using SharedBattleTestFramework;
using Xunit;

namespace BattleshipStateMangementUnitTests
{
	public class PlayerTests
	{
		private IPlayer player;

		private IBoard _board;

		private IFiringBoard _firingBoard;
		public PlayerTests()
		{
			_firingBoard = A.Fake<IFiringBoard>();
			_board = A.Fake<IBoard>();
			player = new Player(_board, _firingBoard);
		}
		[Fact]
		public void PlaceBattleship_onUnOccupiedLocation_DoesNotThrowExecption()
		{
			//Arrange
			var expectedCoordinates = new List<Coordinate>
			{
				new Coordinate(0,0),
				new Coordinate(0,1)
			};
			var exceptedShip = new Ship
			{
				Name = "DESTORYER",
				Size = 2,
				HitCount = 0,
				Position = expectedCoordinates
			};

			//Act
			player.PlaceBattleShip(new Coordinate(0, 0), Orientation.Vertical, ShipType.DESTORYER);

			//Assert
			A.CallTo(() => _board.AddShip(A<Ship>.That.Matches(ship => shipEqualitiyComparater(ship,exceptedShip)))).MustHaveHappened();
		}

		private bool shipEqualitiyComparater (Ship ship1, Ship ship2)
        {
			return ship1.Name == ship2.Name && 
				   ship1.Size == ship2.Size &&
				   ship1.HitCount == ship2.HitCount &&
				   ship1.Position.All(coordinate => ship2.Position.Any(coordiante2 => coordiante2.X == coordinate.X && coordiante2.Y == coordinate.Y));
		}


		[Fact]
		public void PlaceBattleship_onThrownException_ExceptionCaughtAndLogged()
		{
			using (var consoleOutput = new ConsoleOutput())
			{
				//Arrange
				var expectedException = new ShipOccupiedException(new Coordinate(1, 1));
				A.CallTo(() => _board.AddShip(A<Ship>.Ignored)).Throws(expectedException);

				//Act
				player.PlaceBattleShip(new Coordinate(1, 1), Orientation.Vertical, ShipType.BATTLESHIP);
				//Assert
				Assert.Equal("Ship already Occupied at: X:1 Y:1\r\n", consoleOutput.GetOuput());
			}

		}

		[Fact]
		public void PlaceBattleship_onOutOfBoundsLocation_DoesNotAddShipAndLoggedCorrectly()
		{
			using (var consoleOutput = new ConsoleOutput())
			{
				//Arrange
				A.CallTo(() => _board.AddShip(A<Ship>.Ignored)).DoesNothing();

				//Act
				player.PlaceBattleShip(new Coordinate(5, 7), Orientation.Vertical, ShipType.CARRIER);
				var test = consoleOutput.GetOuput();
				//Assert
				A.CallTo(() => _board.AddShip(A<Ship>.Ignored)).MustNotHaveHappened();
				Assert.Equal("The Ship can not be place at 5, 7 in the Vertical orientation\r\n", consoleOutput.GetOuput());
				
			}
		}

		[Fact]
		public void AttackBoard_onCorrectCoordinate_CallsBoardService()
        {
			//Arrange
			A.CallTo(() => _firingBoard.AttackBoard(new Coordinate(0, 1))).Returns( CellStatus.Miss);
			
			//Act
			var res = player.AttackBoard(new Coordinate(0, 1));
			
			//Except
			res.Equals(CellStatus.Miss);
        }

		[Fact]
		public void AttackBoard_OnOutOfRangeCorrectCoordinate_ThrowsExceptionAndDoesNotCallBoardService()
		{
			//Act
			var exception = Assert.Throws<ArgumentOutOfRangeException>(() => player.AttackBoard(new Coordinate(11, 11)));

			//Except
			A.CallTo(() => _firingBoard.AttackBoard(A<Coordinate>.Ignored)).MustNotHaveHappened();
		
		}
	}
}
