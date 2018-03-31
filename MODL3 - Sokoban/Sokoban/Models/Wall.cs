using Sokoban.Enums;

namespace Sokoban.Models
{
	public class Wall : Field
    {
        public Wall(Coordinate coordinate) : base(coordinate)
        {
            Type = FieldType.Wall;
            CanWalkOn = false;
        }

		public override char GetIdentifier()
		{
		    return '█';
		}
	}
}

