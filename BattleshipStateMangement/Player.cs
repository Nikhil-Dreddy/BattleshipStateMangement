using System;
using System.Linq;
using BattleshipStateMangement.Exceptions;
using BattleshipStateMangement.Interfaces;
using BattleshipStateMangement.Models;
using static BattleshipStateMangement.Extensions.CoordinateExtensions;
namespace BattleshipStateMangement
{
	public class Player : IPlayer
	{
		private IBoard _board;

		private IFiringBoard _firingBoard;

		private BoardDimensions _boardDimensions;
		public Player(IBoard board, IFiringBoard firingBoard)
		{
			_board = board;
			_firingBoard = firingBoard;
			_boardDimensions = _board.getDimensions();
		}

		public Player()
		{
			_board = new Board();
		}

		public CellStatus AttackBoard(Coordinate coordinate)
		{
			if(coordinate.X > _boardDimensions.Widget || coordinate.Y > _boardDimensions.Height)
            {
				throw new ArgumentOutOfRangeException($"{coordinate.X}, {coordinate.Y} is out of range of the board ");
            }
			var status = _firingBoard.AttackBoard(coordinate);
			Console.WriteLine($"The attack at {coordinate.X}, {coordinate.Y} was a {status}");
			if(_firingBoard.AllShipsSunk())
				{
					Console.WriteLine("All ships have been sunk, you win!");
				}
				return status;
		}
		
		public void PlaceBattleShip(Coordinate coordinate,Orientation orientation,ShipType shipType)
		{
			var boardDimensions = _board.getDimensions();
			var coordinates = orientation == Orientation.Horizontal ? coordinate.getHorizontalCoordinates(orientation, shipType, boardDimensions) : coordinate.getVerticalCoordinates( orientation, shipType, boardDimensions);
			try
			{
				if (coordinates.Any())
				{
					_board.AddShip(new Ship
					{
						Position = coordinates,
						Name = shipType.ToString(),
						Size = (int)shipType,
						HitCount = 0
					});
				}
			}
			catch (ShipOccupiedException e)
			{
				Console.WriteLine(e.Message);
			}
		}
		
	}
}
