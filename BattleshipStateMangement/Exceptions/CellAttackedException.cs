using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleshipStateMangement.Models;

namespace BattleshipStateMangement.Exceptions
{
    public class CellAttackedException : Exception
    {
        public CellAttackedException(Coordinate coordiante) : base(string.Format($"Cell at: X:{coordiante.X} Y:{coordiante.Y} already attacked"))
        {
        }
    }
}
