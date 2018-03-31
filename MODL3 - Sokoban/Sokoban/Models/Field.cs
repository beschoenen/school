using Sokoban.Enums;

namespace Sokoban.Models
{
	public abstract class Field
	{
		public bool CanWalkOn = true;

		public FieldType Type
		{
			get;
			set;
		}

		public Coordinate Coordinate
		{
			get;
			set;
		}

	    protected Field(Coordinate coordinate)
        {
            Coordinate = coordinate;
        }

		public virtual char GetIdentifier()
		{
            return '·';
		}

	}
}

