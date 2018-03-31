using System;
using System.Collections.Generic;
using System.Linq;
using ALGAdungeon.Source.Algorithms;
using ALGAdungeon.Source.Helpers;
using ALGAdungeon.Source.Types;

namespace ALGAdungeon.Source
{
    public class Map
    {
        private readonly int _width;
        private readonly int _height;

        private List<Room> _rooms;
        private readonly List<Hall> _halls;

        private readonly Room _exit;

        public readonly Player Player;

        public readonly Dijkstra Compass;

        public Map(int width, int height)
        {
            _width = width;
            _height = height;

            _rooms = new List<Room>();
            _halls = new List<Hall>();

            GenerateMap();

            _exit = _rooms[new BetterRandom().Next(_rooms.Count)];
            _exit.IsExit = true;

            var start = _rooms[new BetterRandom().Next(_rooms.Count)];
            Player = new Player(start);

            Compass = new Dijkstra(_rooms, _halls);
        }

        private void GenerateMap()
        {
            for (var x = 1; x <= _width; x++)
            {
                for (var y = 1; y <= _height; y++)
                {
                    _rooms.Add(new Room(new Coordinate(x, y)));
                }
            }

            // Spanning tree bouwen
            _rooms.ForEach(room =>
            {
                // Set left
                if (room.Coordinate.X > 1)
                {
                    var adjacentRoom = Find(room.Coordinate.X - 1, room.Coordinate.Y);
                    room.Left = adjacentRoom.Right ?? NewHall();
                }

                // Set right
                if (room.Coordinate.X < _width)
                {
                    var adjacentRoom = Find(room.Coordinate.X + 1, room.Coordinate.Y);
                    room.Right = adjacentRoom.Left ?? NewHall();
                }

                // Set top
                if (room.Coordinate.Y > 1)
                {
                    var adjacentRoom = Find(room.Coordinate.X, room.Coordinate.Y - 1);
                    room.Top = adjacentRoom.Bottom ?? NewHall();
                }

                // Set bottom
                if (room.Coordinate.Y < _height)
                {
                    var adjacentRoom = Find(room.Coordinate.X, room.Coordinate.Y + 1);
                    room.Bottom = adjacentRoom.Top ?? NewHall();
                }
            });

            _rooms = new SpanningTree(_rooms).Create(_width, _height);
            RemoveHalls();
        }

        private void RemoveHalls()
        {
            var remove = new List<Hall>();

            foreach (var hall in _halls.Where(hall => !hall.PartOfTree))
            {
                if (new BetterRandom().Next(100) < 50) continue;

                if (hall.IsHorizontal)
                {
                    hall.Top.Left = null;
                    hall.Bottom.Right = null;
                }
                else
                {
                    hall.Top.Bottom = null;
                    hall.Bottom.Top = null;
                }

                remove.Add(hall);
            }

            remove.ForEach(r => _halls.Remove(r));
        }

        private Room Find(int x, int y) => Find(new Coordinate(x, y));
        private Room Find(Coordinate coordinate) => _rooms.First(room => Equals(room.Coordinate, coordinate));

        private Hall NewHall()
        {
            var hall = new Hall();

            _halls.Add(hall);

            return hall;
        }

        public string ThrowGrenade()
        {
            var eligableHalls = _halls.Where(hall => !hall.PartOfTree && !hall.IsDestroyed).ToList();

            if (!eligableHalls.Any())
            {
                return "You fear the whole dungeon would collapse if you threw another grenade.";
            }

            var destroyed = 0;
            while (destroyed < Math.Min(15, eligableHalls.Count) && eligableHalls.Count > 0)
            {
                var index = new BetterRandom().Next(eligableHalls.Count);

                eligableHalls[index].IsDestroyed = true;
                eligableHalls.RemoveAt(index);

                destroyed++;
            }

            // Kill enemy
            Player.Position.Enemy = null;

            DrawCompassRoute();

            return "You hear a trembling in the distance, a part of the dungeon has collapsed.";
        }

        public string Talisman()
        {
            return BreathFirstSearch.Talisman(Player);
        }

        public string ToggleCompass()
        {
            if (!Compass.Enabled)
            {
                Compass.Enable();
                DrawCompassRoute();
                return "You get the compass from your pocket, it shows you the way to the exit.";
            }

            Compass.Disable();
            return "You put the compass back into your pocket.";
        }

        public void MovePlayer(Coordinate coordinate)
        {
            var position = Find(coordinate);

            Player.MoveTo(position);

            DrawCompassRoute();
        }

        public string MovePlayer(DirectionType direction)
        {
            var message = Player.Move(direction);

            DrawCompassRoute();

            return message;
        }

        private void DrawCompassRoute()
        {
            if (!Compass.Enabled) return;

            Compass.Show(Player.Position, _exit);
        }

        public override string ToString()
        {
            var map = "\n";

            for (var y = 1; y <= _height; y++)
            {
                var bottomLine = "";

                for (var x = 1; x <= _width; x++)
                {
                    var room = Find(x, y);

                    var roomMark = Player.OnRoom(room) ? "X" : room.ToString();
                    map += $" {roomMark} ";

                    if (x > _width) continue;

                    map += room.Right?.ToString() ?? " ";

                    if (y >= _height) continue;

                    bottomLine += $" {room.Bottom?.ToString() ?? " "}  ";
                }

                map += $"\n{bottomLine}\n";
            }

            return map;
        }
    }
}