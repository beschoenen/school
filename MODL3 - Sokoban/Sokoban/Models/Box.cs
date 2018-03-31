using Sokoban.Enums;

namespace Sokoban.Models
{
	public class Box : Floor
	{
        public Box(Coordinate coordinate) : base(coordinate)
        {
            Type = FieldType.Box;
        }

		public override char GetIdentifier()
		{
		    if (HasMonkey)
		    {
                return '@';
		    }
            
            if (HasBanana)
		    {
		        return '0';
		    }

            return 'X';
		}

	}
}

