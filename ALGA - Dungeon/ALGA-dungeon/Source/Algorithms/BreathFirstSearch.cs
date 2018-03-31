using System.Collections.Generic;
using System.Linq;

namespace ALGAdungeon.Source.Algorithms
{
    public static class BreathFirstSearch
    {
        private struct BfsDepth
        {
            public Room Room;
            public int Depth;
        }

        public static string Talisman(Player player)
        {
            if (player.Position.IsExit)
            {
                return "You are standing in the room with the exit. Open your eyes!";
            }

            var queue = new List<BfsDepth>();
            var visited = new List<BfsDepth>();

            queue.Add(new BfsDepth {Room = player.Position, Depth = 0});

            while (queue.Count > 0)
            {
                var current = queue.First();

                if (current.Room.IsExit)
                {
                    return $"The Talisman lights up and whispers that the exit is {current.Depth} room(s) away from you.";
                }

                visited.Add(current);

                if (current.Room.Top != null &&
                    !visited.Any(x => Equals(x.Room.Coordinate, current.Room.Top.Top.Coordinate)) &&
                    !queue.Any(x => Equals(x.Room.Coordinate, current.Room.Top.Top.Coordinate)))
                {
                    queue.Add(new BfsDepth
                    {
                        Room = current.Room.Top.Top,
                        Depth = current.Depth + 1
                    });
                }

                if (current.Room.Right != null &&
                    !visited.Any(x => Equals(x.Room.Coordinate, current.Room.Right.Top.Coordinate)) &&
                    !queue.Any(x => Equals(x.Room.Coordinate, current.Room.Right.Top.Coordinate)))
                {
                    queue.Add(new BfsDepth
                    {
                        Room = current.Room.Right.Top,
                        Depth = current.Depth + 1
                    });
                }

                if (current.Room.Bottom != null &&
                    !visited.Any(x => Equals(x.Room.Coordinate, current.Room.Bottom.Bottom.Coordinate)) &&
                    !queue.Any(x => Equals(x.Room.Coordinate, current.Room.Bottom.Bottom.Coordinate)))
                {
                    queue.Add(new BfsDepth
                    {
                        Room = current.Room.Bottom.Bottom,
                        Depth = current.Depth + 1
                    });
                }

                if (current.Room.Left != null &&
                    !visited.Any(x => Equals(x.Room.Coordinate, current.Room.Left.Bottom.Coordinate)) &&
                    !queue.Any(x => Equals(x.Room.Coordinate, current.Room.Left.Bottom.Coordinate)))
                {
                    queue.Add(new BfsDepth
                    {
                        Room = current.Room.Left.Bottom,
                        Depth = current.Depth + 1
                    });
                }

                queue.Remove(current);
            }

            return "No exit has been found! You are trapped forever";
        }
    }
}