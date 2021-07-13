using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleshipStateMangement.Models;

namespace BattleshipStateMangement.Interfaces
{
	public interface IPlayer
	{
		public void PlaceBattleShip(Coordinate coordinate, Orientation orientation, ShipType shipType);
		public CellStatus AttackBoard(Coordinate coordinate);

	}
}
