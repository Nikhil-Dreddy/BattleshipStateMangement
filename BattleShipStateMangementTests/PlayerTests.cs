using System;
using BattleshipStateMangement;
using BattleshipStateMangement.Interfaces;
using Xunit;
using FakeItEasy;
using BattleshipStateMangement.Models;
using System.Collections.Generic;

namespace BattleShipStateMangementTests
{
	public class PlayerTests
	{
		private IPlayer player;

		private IBoard _board ;
		public PlayerTests()
		{
			_board = A.Fake<IBoard>();
			player = new Player();
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
				Name = "BattleShip",
				Size = 2,
				HitCount = 0,
				Position = expectedCoordinates
			};

			//Act
			player.PlaceBattleShip(new Coordinate(0,0),Orientation.Vertical,ShipType.BATTLESHIP);
			
			//Assert
			A.CallTo(() => _board.AddShip(exceptedShip)).MustHaveHappened();
		}

		[Fact]
		public void PlaceBattleship_onOccupiedLocation_CatchesExcpetionAndLoggedCorrectly()
		{


		}

		[Fact]
		public void PlaceBattleship_onOutOfBoundsLocation_DoesNotAddShipAndLoggedCorrectly()
		{

		}
	}
}
