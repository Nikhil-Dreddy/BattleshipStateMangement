using System.Collections.Generic;
using BattleshipStateMangement.Models;

namespace BattleshipStateMangement.Interfaces
{
	public interface IBoard
	{
		public void CreateBoard();

		public void AddShip(Ship ship);

		public BoardDimensions getDimensions();

		public IEnumerable<Ship> getShips();

	}
}
