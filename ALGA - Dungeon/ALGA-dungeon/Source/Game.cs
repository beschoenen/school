using System;
using System.Linq;
using ALGAdungeon.Source.Helpers;
using ALGAdungeon.Source.Types;

namespace ALGAdungeon.Source
{
    public class Game
    {
        public static bool Cheat { get; set; }

        public bool Over { get; set; }

        public Map Map { get; }

        public string Actions => string.Join(", ", Map.Player.Position.Actions);

        public string LastAction { get; set; }

        public Game(int width, int height)
        {
            Map = new Map(width, height);
        }

        public bool Play(string input)
        {
            LastAction = "";

            if (input.ToLower().StartsWith("move"))
            {
                try
                {
                    var coord = input.Split(' ')[1].Split(',');

                    Map.MovePlayer(new Coordinate(int.Parse(coord[0]), int.Parse(coord[1])));
                }
                catch (Exception)
                {
                    return false;
                }

                return true;
            }

            if (input.ToLower() == "cheat")
            {
                Cheat = !Cheat;
                return true;
            }

            var action = Map.Player.Position.Actions.FirstOrDefault(e => string.Equals(e, input, StringComparison.CurrentCultureIgnoreCase));

            if (action == null) return false;

            switch (action)
            {
                case "Attack":
                    LastAction = Map.Player.Attack();
                    break;
                case "North":
                    LastAction = Map.MovePlayer(DirectionType.North);
                    break;
                case "East":
                    LastAction = Map.MovePlayer(DirectionType.East);
                    break;
                case "South":
                    LastAction = Map.MovePlayer(DirectionType.South);
                    break;
                case "West":
                    LastAction = Map.MovePlayer(DirectionType.West);
                    break;
                case "Talisman":
                    LastAction = Map.Talisman();
                    break;
                case "Grenade":
                    LastAction = Map.ThrowGrenade();
                    break;
                case "Compass":
                    LastAction = Map.ToggleCompass();
                    break;
                case "Exit":
                    LastAction = "\nExit found!";
                    Over = true;
                    break;
                default:
                    LastAction = "Invalid action";
                    break;
            }

            // Check if enemy died during round.
            if (Map.Player.Health < 1)
            {
                Over = true;
            }

            return true;
        }

        public override string ToString()
        {
            var text = Map.ToString();

            text += $"You have {Map.Player.Health} health.\n\n";

            if (LastAction != null)
            {
                text += $"{LastAction}\n\n";
            }

            text += $"{Map.Player.Position.Description}\n\n";

            text += $"The following actions are available:\n{Actions}";

            return text;
        }
    }
}