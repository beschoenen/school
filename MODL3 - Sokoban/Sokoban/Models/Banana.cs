using Sokoban.Enums;

namespace Sokoban.Models
{
	public class Banana
	{
	    public virtual Coordinate Coordinate { get; private set; }

        public Banana(Coordinate coordinate)
        {
            Coordinate = coordinate;
        }

		public virtual void Move(DirectionType direction)
		{
            Coordinate = Game.CalculateNewCoordinate(Coordinate, direction);
		}

	}
}

