using System.Collections.Generic;
using ALGAdungeon.Source.Helpers;
using ALGAdungeon.Source.Types;

namespace ALGAdungeon.Source.Algorithms
{
    public class Dijkstra
    {
        private struct Vertex
        {
            public Room Room;
            public int Resistance;
        }

        public bool Enabled { get; private set; }

        private readonly List<Room> _rooms;
        private readonly List<Hall> _halls;

        public Dijkstra(List<Room> rooms, List<Hall> halls)
        {
            _rooms = rooms;
            _halls = halls;
        }

        public void Enable()
        {
            Enabled = true;
        }

        public void Disable()
        {
            _halls.ForEach(h => h.ShowHint = false);
            Enabled = false;
        }

        public void Show(Room start, Room exit)
        {
            _halls.ForEach(h => h.ShowHint = false);

            var rooms = ShortedPath(BuildVertices(), start.Coordinate, exit.Coordinate);
            rooms.Add(start);

            for (var i = 0; i < rooms.Count - 1; i++)
            {
                if (rooms[i].Top != null && Equals(rooms[i].Top?.Top.Coordinate, rooms[i + 1].Coordinate))
                {
                    rooms[i].Top.ShowHint = true;
                    continue;
                }

                if (rooms[i].Right != null && Equals(rooms[i].Right?.Top.Coordinate, rooms[i + 1].Coordinate))
                {
                    rooms[i].Right.ShowHint = true;
                    continue;
                }

                if (rooms[i].Bottom != null && Equals(rooms[i].Bottom?.Bottom.Coordinate, rooms[i + 1].Coordinate))
                {
                    rooms[i].Bottom.ShowHint = true;
                    continue;
                }

                if (rooms[i].Left != null && Equals(rooms[i].Left?.Bottom.Coordinate, rooms[i + 1].Coordinate))
                {
                    rooms[i].Left.ShowHint = true;
                }
            }
        }

        private List<Room> ShortedPath(Dictionary<Room, List<Vertex>> vertices, Coordinate start, Coordinate end)
        {
            var previous = new Dictionary<Room, Room>();
            var distances = new Dictionary<Room, int>();
            var nodes = new List<Room>();

            List<Room> path = null;

            // Set all distances to max, except the start point
            foreach (var vertex in vertices)
            {
                distances[vertex.Key] = Equals(vertex.Key.Coordinate, start) ? 0 : int.MaxValue;
                nodes.Add(vertex.Key);
            }

            // Loop through all nodes
            while (nodes.Count != 0)
            {
                // Sort the nodes by distance
                nodes.Sort((x, y) => distances[x] - distances[y]);

                var smallest = nodes[0];
                nodes.Remove(smallest);

                // Find the end point
                if (Equals(smallest.Coordinate, end))
                {
                    path = new List<Room>();
                    while (previous.ContainsKey(smallest))
                    {
                        path.Add(smallest);
                        smallest = previous[smallest];
                    }

                    break;
                }

                if (distances[smallest] == int.MaxValue) break;

                foreach (var neighbor in vertices[smallest])
                {
                    var alt = distances[smallest] + neighbor.Resistance;
                    if (alt < distances[neighbor.Room])
                    {
                        distances[neighbor.Room] = alt;
                        previous[neighbor.Room] = smallest;
                    }
                }
            }

            return path;
        }

        private Dictionary<Room, List<Vertex>> BuildVertices()
        {
            var vectices = new Dictionary<Room, List<Vertex>>();

            // Loop through all rooms and their halls to add them to the the vertices
            // Only rooms that are walkable are added
            _rooms.ForEach(room =>
            {
                var directions = new List<Vertex>();

                if (room.Top?.Top != null && room.Top.Walkable)
                {
                    directions.Add(new Vertex
                    {
                        Room = room.Top.Top,
                        Resistance = room.Top.Resistance(DirectionType.North)
                    });
                }

                if (room.Right?.Top != null && room.Right.Walkable)
                {
                    directions.Add(new Vertex
                    {
                        Room = room.Right.Top,
                        Resistance = room.Right.Resistance(DirectionType.East)
                    });
                }

                if (room.Bottom?.Bottom != null && room.Bottom.Walkable)
                {
                    directions.Add(new Vertex
                    {
                        Room = room.Bottom.Bottom,
                        Resistance = room.Bottom.Resistance(DirectionType.South)
                    });
                }

                if (room.Left?.Bottom != null && room.Left.Walkable)
                {
                    directions.Add(new Vertex
                    {
                        Room = room.Left.Bottom,
                        Resistance = room.Left.Resistance(DirectionType.West)
                    });
                }

                vectices.Add(room, directions);
            });

            return vectices;
        }
    }
}