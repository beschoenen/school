using Sokoban.Enums;

namespace Sokoban.Models
{
	public class Floor : Field
	{
		public bool HasBanana { get; set; }

		public bool HasMonkey { get; set; }

        public Floor(Coordinate coordinate) : base(coordinate)
        {
            Type = FieldType.Floor;
        }

		public override char GetIdentifier()
		{
		    if (HasMonkey)
		    {
                return '@';
            }
            
            if (HasBanana)
		    {
		        return 'O';
		    }

		    return base.GetIdentifier();
		}
	}
}

