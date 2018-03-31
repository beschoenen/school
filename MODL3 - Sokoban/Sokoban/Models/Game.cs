using System;
using System.IO;
using System.Linq;
using Sokoban.Enums;
using System.Collections.Generic;

namespace Sokoban.Models
{
    public class Game
    {
        private Maze Maze;

        private Monkey Monkey;

        private List<Banana> Bananas;

        public bool IsPlaying { get; private set; }

        public Game()
        {
            IsPlaying = true;
        }

        private void CheckEndGame()
        {
            if (Maze.AllBoxesHaveBananas())
            {
                IsPlaying = false;
            }
        }

        //////////////////////
        // Map loading methods

        public bool LoadMaze(int maze)
        {
            var lines = new List<string>();
            try
            {
                lines = File.ReadAllLines(string.Format("doolhof/doolhof{0}.txt", maze)).ToList();
            }
            catch (Exception)
            {
                Console.WriteLine("Kon 'doolhof/doolhof{0}.txt' niet vinden", maze);
                Console.ReadKey();

                return false;
            }

            ParseMaze(lines);

            return true;
        }

        private void ParseMaze(IReadOnlyCollection<string> lines)
        {
            Maze = new Maze();
            Bananas = new List<Banana>();

            var y = lines.Count;
            foreach (var line in lines)
            {
                var x = 1;
                foreach (var character in line.ToCharArray())
                {
                    var coordinate = new Coordinate(x, y);
                    switch (character)
                    {
                        case '#'://Wall
                            Maze.Map.Add(coordinate.ToString(), new Wall(coordinate));
                            break;
                        case '.'://Empty floor
                            Maze.Map.Add(coordinate.ToString(), new Floor(coordinate));
                            break;
                        case 'o'://Banana + Floor
                            Maze.Map.Add(coordinate.ToString(), new Floor(coordinate) {HasBanana = true});
                            Bananas.Add(new Banana(coordinate));
                            break;
                        case '@'://Monkey + Floor
                            Maze.Map.Add(coordinate.ToString(), new Floor(coordinate) {HasMonkey = true});
                            Monkey = new Monkey(coordinate);
                            break;
                        case 'x'://Box
                            Maze.Map.Add(coordinate.ToString(), new Box(coordinate));
                            break;
                        case ' '://Space
                            Maze.Map.Add(coordinate.ToString(), new Space(coordinate));
                            break;
                        default:
                            throw new IndexOutOfRangeException();
                    }
                    x++;
                }
                y--;
            }
        }

        ///////////////
        // Move Methods

        public void MoveMonkey(ConsoleKey input)
        {
            var direction = ConvertInputToDirection(input);
            var coordinate = new Coordinate(Monkey.Coordinate.X, Monkey.Coordinate.Y);

            var nextField = Maze.GetNextField(coordinate, direction);
            if (nextField.Type == FieldType.Wall) return;

            var canMove = nextField.CanWalkOn;

            var nextFloor = (Floor) nextField;
            if (nextFloor.HasBanana)
            {
                canMove = MoveBanana(nextFloor.Coordinate, direction);
            }

            if (canMove)
            {
                ((Floor) Maze.Map[Monkey.Coordinate.ToString()]).HasMonkey = false;
                Monkey.Walk(direction);
                nextFloor.HasMonkey = true;
            }
        }

        private bool MoveBanana(Coordinate coordinate, DirectionType direction)
        {
            var banana = Bananas.First(b => b.Coordinate.ToString() == coordinate.ToString());
            var nextField = Maze.GetNextField(coordinate, direction);

            if (nextField.Type == FieldType.Wall) return false;

            var nextFloor = (Floor) nextField;
            if (!nextFloor.HasBanana)
            {
                ((Floor) Maze.Map[coordinate.ToString()]).HasBanana = false;
                banana.Move(direction);
                nextFloor.HasBanana = true;

                if (nextFloor.Type == FieldType.Box && Maze.AllBoxesHaveBananas())
                {
                    IsPlaying = false;
                }

                return true;
            }

            return false;
        }

        /////////////////
        // Helper methods

        public string GetMap()
        {
            var map = "";

            Maze.Map.GroupBy(item => item.Value.Coordinate.Y).ToList().ForEach(row =>
            {
                row.ToList().ForEach(field =>
                {
                    map += field.Value.GetIdentifier();
                });
                map += "\n";
            });

            return map;
        }

        public static Coordinate CalculateNewCoordinate(Coordinate coordinate, DirectionType direction)
        {
            var coord = new Coordinate(coordinate.X, coordinate.Y);
            switch (direction)
            {
                case DirectionType.Up:
                    coord.Y++;
                    break;
                case DirectionType.Down:
                    coord.Y--;
                    break;
                case DirectionType.Left:
                    coord.X--;
                    break;
                case DirectionType.Right:
                    coord.X++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("direction", direction, null);
            }

            return coord;
        }

        public static DirectionType ConvertInputToDirection(ConsoleKey input)
        {
            if (new[] { ConsoleKey.UpArrow, ConsoleKey.W }.Contains(input))
            {
                return DirectionType.Up;
            }

            if (new[] { ConsoleKey.DownArrow, ConsoleKey.S }.Contains(input))
            {
                return DirectionType.Down;
            }

            if (new[] { ConsoleKey.LeftArrow, ConsoleKey.A }.Contains(input))
            {
                return DirectionType.Left;
            }

            if (new[] { ConsoleKey.RightArrow, ConsoleKey.D }.Contains(input))
            {
                return DirectionType.Right;
            }

            throw new ArgumentOutOfRangeException("input", input, null);
        }

    }
}

