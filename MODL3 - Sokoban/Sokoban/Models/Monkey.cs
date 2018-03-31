using Sokoban.Enums;

namespace Sokoban.Models
{
    public class Monkey
    {
        public virtual Coordinate Coordinate { get; private set; }

        public Monkey(Coordinate coordinate)
        {
            Coordinate = coordinate;
        }

        public virtual void Walk(DirectionType direction)
        {
            Coordinate = Game.CalculateNewCoordinate(Coordinate, direction);
        }

    }
}

