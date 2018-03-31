using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sokoban.Enums;

namespace Sokoban.Models
{
    class Space : Field
    {
        public Space(Coordinate coordinate) : base(coordinate)
        {
            CanWalkOn = false;

            Type = FieldType.Space;
        }

        public override char GetIdentifier()
        {
            return ' ';
        }
    }
}
