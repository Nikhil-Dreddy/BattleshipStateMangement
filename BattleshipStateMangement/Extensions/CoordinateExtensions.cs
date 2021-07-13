using System;
using System.Collections.Generic;
using BattleshipStateMangement.Models;

namespace BattleshipStateMangement.Extensions
{
    public static class CoordinateExtensions
    {
		public static IEnumerable<Coordinate> getVerticalCoordinates(this Coordinate coordinate, Orientation orientation, ShipType shipType, BoardDimensions boardDimensions)
		{
			var shipCoordiantes = new List<Coordinate>();
			for (int j = coordinate.Y; j < ((int)shipType + coordinate.Y); j++)
			{
				if (j > boardDimensions.Height)
				{
					shipCoordiantes.Clear();
					Console.WriteLine($"The Ship can not be place at {coordinate.X}, {coordinate.Y} in the {orientation} orientation");
					break;
				}
				shipCoordiantes.Add(new Coordinate(coordinate.X, j));
			}
			return shipCoordiantes;
		}

		public static IEnumerable<Coordinate> getHorizontalCoordinates(this Coordinate coordinate, Orientation orientation, ShipType shipType, BoardDimensions boardDimensions)
		{
			var shipCoordiantes = new List<Coordinate>();
			for (int i = coordinate.X; i < ((int)shipType + coordinate.X); i++)
			{
				if (i > boardDimensions.Widget)
				{
					shipCoordiantes.Clear();
					Console.WriteLine($"The Ship can not be place at {coordinate.X}, {coordinate.Y} in the ${orientation} oreintation");
					break;
				}
				shipCoordiantes.Add(new Coordinate(i, coordinate.Y));
			}
			return shipCoordiantes;
		}

	}
}
