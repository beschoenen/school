using System.Collections.Generic;
using System.Linq;
using ALGAdungeon.Source.Helpers;

namespace ALGAdungeon.Source.Algorithms
{
    public class SpanningTree
    {
        private readonly List<Room> _rooms;

        public SpanningTree(List<Room> rooms)
        {
            _rooms = rooms;
        }

        public List<Room> Create(int width, int height)
        {
            var spanningTree = new List<TreeConnection>();

            for (var i = 1; i < height + 1; i++)
            {
                for (var j = 1; j < width + 1; j++)
                {
                    spanningTree = CreateTreeConnection(new Coordinate(j, i), spanningTree);
                }
            }

            return _rooms;
        }

        private Room Find(Coordinate coordinate) => _rooms.First(room => Equals(room.Coordinate, coordinate));

        private List<TreeConnection> CreateTreeConnection(Coordinate coordinate, List<TreeConnection> spanningTree)
        {
            var room = Find(coordinate);

            var resistance = 10;
            var addConnection = false;

            Room linkTo = null;
            Hall connection = null;

            // Select a room to connect to
            if (room.Top != null && room.Top.Resistance(Types.DirectionType.North) <= resistance &&
                !ConnectionExist(room, room.Top.Top, spanningTree))
            {
                linkTo = room.Top.Top;
                connection = room.Top;
                addConnection = true;
                resistance = room.Top.Resistance(Types.DirectionType.North);
            }

            if (room.Right != null && room.Right.Resistance(Types.DirectionType.East) <= resistance &&
                !ConnectionExist(room, room.Right.Top, spanningTree))
            {
                linkTo = room.Right.Top;
                connection = room.Right;
                addConnection = true;
                resistance = room.Right.Resistance(Types.DirectionType.East);
            }

            if (room.Bottom != null && room.Bottom.Resistance(Types.DirectionType.South) <= resistance &&
                !ConnectionExist(room, room.Bottom.Bottom, spanningTree))
            {
                linkTo = room.Bottom.Bottom;
                connection = room.Bottom;
                addConnection = true;
                resistance = room.Bottom.Resistance(Types.DirectionType.South);
            }

            if (room.Left != null && room.Left.Resistance(Types.DirectionType.West) <= resistance &
                !ConnectionExist(room, room.Left.Bottom, spanningTree))
            {
                linkTo = room.Left.Bottom;
                connection = room.Left;
                addConnection = true;
            }

            // Make the connection
            if (addConnection)
            {
                spanningTree.Add(new TreeConnection(room, linkTo));
            }

            // Set the hall as part of the ST
            if (connection != null)
            {
                connection.PartOfTree = true;
            }

            return spanningTree;
        }

        private bool ConnectionExist(Room room, Room linkTo, List<TreeConnection> spanningTree)
        {
            return spanningTree.Any(connection =>
                connection.GetRoom1() == room && connection.GetRoom2() == linkTo ||
                connection.GetRoom1() == linkTo && connection.GetRoom2() == room);
        }
    }
}