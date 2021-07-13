namespace BattleshipStateMangement.Models
{
	public class Cell
	{
		public Cell(Coordinate coordinate, CellStatus cellStatus = CellStatus.Empty)
		{
			Coordinate = coordinate;
			CellStatus = cellStatus;
		}
		public Coordinate Coordinate { get; set; }

		public CellStatus CellStatus { get; set; }

		public Ship OccupyingShip { get; set; }
	}
}
