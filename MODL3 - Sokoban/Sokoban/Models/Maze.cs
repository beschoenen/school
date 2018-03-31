using System;
using System.Collections.Generic;
using System.Linq;
using Sokoban.Enums;

namespace Sokoban.Models
{
	public class Maze
	{
		public Dictionary<string, Field> Map;

	    public Maze()
	    {
	        Map = new Dictionary<string, Field>();
	    }

		public bool AllBoxesHaveBananas()
		{
		    return Map.Where(field => field.Value.Type == FieldType.Box).All(field => ((Box) field.Value).HasBanana);
		}

        public virtual Field GetNextField(Coordinate coordinate, DirectionType direction)
        {
            return Map[Game.CalculateNewCoordinate(coordinate, direction).ToString()];
        }

	}
}
