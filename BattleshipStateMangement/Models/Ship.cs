using System;
using System.Collections.Generic;

namespace BattleshipStateMangement.Models
{
	public class Ship
	{
		public string Name { get; set; }

		public IEnumerable<Coordinate> Position { get; set; }

		public int Size { get; set; }
		
		public int HitCount { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Ship ship &&
                   Name == ship.Name &&
                   EqualityComparer<IEnumerable<Coordinate>>.Default.Equals(Position, ship.Position) &&
                   Size == ship.Size &&
                   HitCount == ship.HitCount;
        }

      


        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Position, Size, HitCount);
        }
    }
}
