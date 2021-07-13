using BattleshipStateMangement.Models;

namespace BattleshipStateMangement.Interfaces
{
	interface ICell
	{
		public CellStatus GetCellStatus { get; set; }
		public Ship GetShip { get; set; }
	}
}
