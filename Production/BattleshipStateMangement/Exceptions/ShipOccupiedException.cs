using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleshipStateMangement.Models;

namespace BattleshipStateMangement.Exceptions
{
	public class ShipOccupiedException : Exception
	{
		public ShipOccupiedException(Coordinate coordiante) : base(string.Format($"Ship already Occupied at: X:{coordiante.X} Y:{coordiante.Y}"))
		{
		}
	}
}
