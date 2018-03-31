using System;
using ALGAdungeon.Source.Types;

namespace ALGAdungeon.Source
{
    public class Hall : Tile
    {
        public Room Top { get; set; }
        public Room Bottom { get; set; }

        public bool IsHorizontal { get; set; }
        public bool IsDestroyed { get; set; }

        public bool PartOfTree { get; set; }

        public bool Walkable => !IsDestroyed;

        public bool ShowHint { get; set; }

        public Room Walk(DirectionType direction)
        {
            IsExplored = true;

            switch (direction)
            {
                case DirectionType.North:
                case DirectionType.East:
                    return Top.Walk();
                case DirectionType.South:
                case DirectionType.West:
                    return Bottom.Walk();
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public int Resistance(DirectionType direction)
        {
            switch (direction)
            {
                case DirectionType.North:
                case DirectionType.East:
                    return Top.Enemy?.Level ?? 0;
                case DirectionType.South:
                case DirectionType.West:
                    return Bottom.Enemy?.Level ?? 0;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public override string ToString()
        {
            if (IsDestroyed)
            {
                return "~";
            }

            if (!Game.Cheat && !IsExplored && !ShowHint)
            {
                return " ";
            }

            return IsHorizontal ? "-" : "|";
        }
    }
}