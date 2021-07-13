using System.Collections.Generic;
using BattleshipStateMangement.Interfaces;
using BattleshipStateMangement.Models;
using BattleshipStateMangement.Exceptions;
using System.Linq;

namespace BattleshipStateMangement
{
	public class Board : IBoard,IFiringBoard
	{
		private readonly Dictionary<Coordinate, Cell> _board = new Dictionary<Coordinate, Cell>();
		private readonly List<Ship> _ships = new List<Ship>();
		private readonly BoardDimensions _boardDimensions = new();
		public Board(BoardDimensions dimensions)
		{
			_boardDimensions = dimensions;
		}

		public Board()
		{
		}

		public void AddShip(Ship ship)
		{
			if (NotOccupied(ship))
			{
				AddShipToBoard(ship);
			};
		}

		private bool NotOccupied(Ship ship)
		{
			foreach (Coordinate coordinate in ship.Position)
			{
					if (_board[coordinate].CellStatus == CellStatus.Occupied)
					{
						throw new ShipOccupiedException(coordinate);
					}
			}
			return true;
		}

		private void AddShipToBoard(Ship ship)
		{
			foreach (Coordinate coordinate in ship.Position)
			{
				_board[coordinate].CellStatus = CellStatus.Occupied;
				_board[coordinate].OccupyingShip = ship;
			}
			_ships.Add(ship);
		}
		public CellStatus AttackBoard(Coordinate coordinate)
		{
			var cell = _board[coordinate];
			if (cell.CellStatus == CellStatus.Empty)
            {
				cell.CellStatus = CellStatus.Miss;
				return CellStatus.Miss;
            }
			else if (cell.CellStatus == CellStatus.Occupied)
			{
				cell.CellStatus = CellStatus.Hit;
				HitShip(cell.OccupyingShip);
				return CellStatus.Hit;
			}
			else
            {
				throw new CellAttackedException(coordinate);
            }
		}

		public bool AllShipsSunk()
        {
			return !_ships.Any();
        }

		public void CreateBoard()
		{
			for (int i = 0; i <= _boardDimensions.Widget; i++)
			{
				for (int j = 0; j <= _boardDimensions.Height; j++)
				{
					_board.Add(new Coordinate(i, j), new Cell (new Coordinate(i,j),CellStatus.Empty));		
				}
			};
		}

		public BoardDimensions getDimensions()
		{
			return _boardDimensions;
		}

		public IEnumerable<Ship> getShips()
		{
			return _ships;
		}

		private void HitShip(Ship ship)
        {
			ship.HitCount += 1;
			if(ship.HitCount >= ship.Size )
            {
				_ships.Remove(ship);
            }
        }
    }
}
