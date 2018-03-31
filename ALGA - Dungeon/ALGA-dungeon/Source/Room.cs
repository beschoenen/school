using System.Collections.Generic;
using ALGAdungeon.Source.Helpers;

namespace ALGAdungeon.Source
{
    public class Room : Tile
    {
        private Hall _top;
        private Hall _right;
        private Hall _bottom;
        private Hall _left;

        public Hall Top
        {
            get => _top;
            set
            {
                _top = value;

                if (value == null) return;
                value.IsHorizontal = false;
                value.Bottom = this;
            }
        }

        public Hall Right
        {
            get => _right;
            set
            {
                _right = value;

                if (value == null) return;
                value.IsHorizontal = true;
                value.Bottom = this;
            }
        }

        public Hall Bottom
        {
            get => _bottom;
            set
            {
                _bottom = value;

                if (value == null) return;
                value.IsHorizontal = false;
                value.Top = this;
            }
        }

        public Hall Left
        {
            get => _left;
            set
            {
                _left = value;

                if (value == null) return;
                value.IsHorizontal = true;
                value.Top = this;
            }
        }

        public bool IsExit { get; set; }

        public Coordinate Coordinate { get; }

        public Enemy Enemy { get; set; }

        public string Description
        {
            get
            {
                var text = "You are in a room ";

                if (Enemy == null)
                {
                    text += "without an enemy";
                }
                else
                {
                    text += $"with {Enemy}";
                }

                if (IsExit)
                {
                    text += ", and a staircase with a bright light shining down";
                }

                return $"{text}. What do you do?";
            }
        }

        public List<string> Actions
        {
            get
            {
                var actions = new List<string> {"Talisman", "Compass"};

                if (Enemy != null)
                {
                    actions.Add("Grenade");
                    actions.Add("Attack");

                    return actions;
                }

                if (IsExit) actions.Add("Exit");
                if (Top?.Walkable ?? false) actions.Add("North");
                if (Right?.Walkable ?? false) actions.Add("East");
                if (Bottom?.Walkable ?? false) actions.Add("South");
                if (Left?.Walkable ?? false) actions.Add("West");

                return actions;
            }
        }

        public Room(Coordinate coordinate)
        {
            Coordinate = coordinate;

            if (new BetterRandom().Next(1, 10) > 6)
            {
                Enemy = new Enemy();
            }
        }

        public Hall Between(Room room)
        {
            if (room.Top?.Top == room)
            {
                return room.Top;
            }

            if (room.Right?.Top == room)
            {
                return room.Right;
            }

            if (room.Bottom?.Bottom == room)
            {
                return room.Bottom;
            }

            if (room.Left?.Bottom == room)
            {
                return room.Left;
            }

            return null;
        }

        public Room Walk()
        {
            IsExplored = true;
            return this;
        }

        public override string ToString()
        {
            if (Game.Cheat)
            {
                if (IsExit)
                {
                    return "H";
                }

                return (Enemy?.Level ?? 0).ToString();
            }

            return !IsExplored ? "·" : "N";
        }
    }
}