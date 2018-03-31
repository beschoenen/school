using Gold_Rush.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Gold_Rush.Enum;

namespace Gold_Rush.View
{
    public class OutputView
    {
        public static void PrintWelcomeMessage()
        {
            Console.WriteLine("Welcome to Gold Fever\n");
            Console.WriteLine("The point of the game is to get as many carts to the ships as possible.");
            Console.WriteLine("For each ship filled you get 10 points, and per cart you get 1 point.");
            Console.WriteLine("Navigate the carts by using the switches. Press 1,2,3,4 or 5 on the keyboard");
            Console.WriteLine("to switch the track's direction.");
            Console.WriteLine("A cart that can't get on a switch will stand still, if 2 carts crash it's");
            Console.WriteLine("game over.");
            Console.WriteLine("You can store 8 carts in the railyard at the bottom, if a 9th arrives it counts as a crash\n");
            Console.WriteLine("Switches are: ┌ , └ , ┐ or ┘");
            Console.WriteLine("Full carts are 'o' empty is 'u'.");
            Console.WriteLine("Full railyards are X and empty is '-'");
            Console.WriteLine("any other '-' is a track along with /, \\ or }");
            Console.WriteLine("The dock is '[ ]' ");
            Console.WriteLine("Goodluck!\n");
        }

        public static void PrintEndGame(int score)
        {
            Console.WriteLine("Game Over! You finished with a score of: {0}", score);
        }

        private static void PrintBoat(Ship ship)
        {
            var side = new string('-', Ship.MaxLoad);
            var load = new string('X', ship.Load) + new string('·', Ship.MaxLoad - ship.Load);

            var shipString = new List<string>
            {
                " /" + side + "\\",
                "{ " + load + " }",
                " \\" + side + "/"
            };

            Console.WriteLine(new string('#', 30));

            var shipOffset = 0;
            var currentWater = ship.Water;
            while (currentWater.Previous != null)
            {
                currentWater = currentWater.Previous as Water;
                shipOffset += 7;
            }

            shipString.ForEach(e => Console.WriteLine("{0}{1}", new string(' ', shipOffset), e)); // Ship
            Console.WriteLine(new string(' ', 4) + "[ ]"); // Dock
        }

        public static void PrintPlayground(PlayingGround playingGround)
        {
            var wareHouseA = playingGround.Warehouses['A'];
            var wareHouseB = playingGround.Warehouses['B'];
            var wareHouseC = playingGround.Warehouses['C'];

            var switchOne = playingGround.Switches[1];
            var switchTwo = playingGround.Switches[2];
            var switchThree = playingGround.Switches[3];
            var switchFour = playingGround.Switches[4];
            var switchFive = playingGround.Switches[5];

            Console.Clear();
            Console.WriteLine("Score: {0}", Game.Score);

            PrintBoat(playingGround.Ship);

            // Line Eight
            var lineEight = switchFive.Tail.Next.Next.Next.Next;
            var currentLineEight = lineEight;

            var lineEightArray = new List<char> { GetTrackIcon(currentLineEight, '\\') };
            for (var i = 0; i < 14; i++)
            {
                currentLineEight = currentLineEight.Next;
                lineEightArray.Add(GetTrackIcon(currentLineEight));
            }
            lineEightArray.Reverse();

            Console.WriteLine(string.Join("", lineEightArray));

            // Line Seven
            Console.WriteLine("{0}{1}", new string(' ', 15), GetTrackIcon(switchFive.Tail.Next.Next.Next, '|'));

            // Line Six
            Console.WriteLine(
                "A{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}", 
                GetTrackIcon(wareHouseA.FirstTrack),
                GetTrackIcon(wareHouseA.FirstTrack.Next),
                GetTrackIcon(wareHouseA.FirstTrack.Next.Next, '\\'),
                new string(' ', 3),
                GetTrackIcon(switchTwo.Top, '/'),
                GetTrackIcon(switchTwo.Top.Next),
                GetTrackIcon(switchTwo.Top.Next.Next),
                GetTrackIcon(switchTwo.Top.Next.Next.Next),
                GetTrackIcon(switchFive.Top, '\\'),
                new string(' ', 3),
                GetTrackIcon(switchFive.Tail.Next.Next, '|')
            );

            // Line Five
            Console.WriteLine(
                "{0}{1}{2}{3}{4}{5}{6}{7}",
                new string(' ', 4),
                GetSwitchIcon(switchOne),
                GetTrackIcon(switchOne.Tail),
                GetSwitchIcon(switchTwo),
                new string(' ', 5),
                GetSwitchIcon(switchFive),
                GetTrackIcon(switchFive.Tail),
                GetTrackIcon(switchFive.Tail.Next, '/')
            );

            // Line Four
            Console.WriteLine(
                "B{0}{1}{2}{3}{4}{5}{6}",
                GetTrackIcon(wareHouseB.FirstTrack),
                GetTrackIcon(wareHouseB.FirstTrack.Next),
                GetTrackIcon(switchOne.Bottom, '/'),
                new string(' ', 3),
                GetTrackIcon(switchTwo.Bottom, '\\'),
                new string(' ', 3),
                GetTrackIcon(switchFive.Bottom, '/')
            );

            // Line Three
            Console.WriteLine(
                "{0}{1}{2}{3}",
                new string(' ', 8),
                GetSwitchIcon(switchThree),
                GetTrackIcon(switchThree.Tail),
                GetSwitchIcon(switchFour)
            );

            // Line Two
            Console.WriteLine(
                "C{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}",
                GetTrackIcon(wareHouseC.FirstTrack),
                GetTrackIcon(wareHouseC.FirstTrack.Next),
                GetTrackIcon(wareHouseC.FirstTrack.Next.Next),
                GetTrackIcon(wareHouseC.FirstTrack.Next.Next.Next),
                GetTrackIcon(wareHouseC.FirstTrack.Next.Next.Next.Next),
                GetTrackIcon(wareHouseC.FirstTrack.Next.Next.Next.Next.Next),
                GetTrackIcon(switchThree.Bottom, '/'),
                new string(' ', 3),
                GetTrackIcon(switchFour.Bottom, '\\'),
                GetTrackIcon(switchFour.Bottom.Next),
                GetTrackIcon(switchFour.Bottom.Next.Next),
                GetTrackIcon(switchFour.Bottom.Next.Next.Next),
                GetTrackIcon(switchFour.Bottom.Next.Next.Next.Next, '\\')
            );


            // Line One
            var lineOne = switchFour.Bottom.Next.Next.Next.Next.Next;
            var current = lineOne;

            var lineOneArray = new List<char> { GetTrackIcon(current, '/') };
            for (var i = 0; i < 15; i++)
            {
                current = current.Next;
                lineOneArray.Add(GetTrackIcon(current));
            }
            lineOneArray.Reverse();

            Console.WriteLine(string.Join("", lineOneArray));
        }

        private static char GetSwitchIcon(Switch gameSwitch)
        {
            if (gameSwitch.Cart != null)
            {
                return gameSwitch.Cart.Load == 0 ? 'u' : 'o';
            }

            if (gameSwitch.SwitchType == SwitchType.Merge)
            {
                return gameSwitch.SwitchState == SwitchState.Bottom ? '┌' : '└';
            }

            return gameSwitch.SwitchState == SwitchState.Bottom ? '┐' : '┘';
        }

        private static char GetTrackIcon(Track track, char defaultChar = '-')
        {
            if (track.Cart != null)
            {
                if (track is Yard && (track.Next == null || track.Next.Cart != null))
                {
                    // Bug: We have to check if the first car in a group is at the end.
                    return 'x';
                }

                return track.Cart.Load == 0 ? 'u' : 'o';
            }

            return defaultChar;
        }
    }
}