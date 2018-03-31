using System;
using System.Collections.Generic;
using Gold_Rush.Enum;
using Gold_Rush.Model;

namespace Gold_Rush
{
    public class Reviver
    {
        private static Dictionary<int, Switch> _switches;
        private static Dictionary<char, Warehouse> _warehouses;
        private static Dock _dock;

        public static PlayingGround GeneratePlayingGround()
        {
            var ship = new Ship();

            _switches = new Dictionary<int, Switch>();
            _warehouses = new Dictionary<char, Warehouse>();
            _dock = new Dock{Ship = ship};

            ship.Water = GenerateWaterRoute();

            GenerateSwitches();
            GenerateRoute();

            var playingGround = new PlayingGround
            {
                Ship = ship,
                Carts = new List<Cart>(),
                Dock = _dock,
                Switches = _switches,
                Warehouses = _warehouses
            };

            return playingGround;
        }

        private static Water GenerateWaterRoute()
        {
            var waterOne = new Water{Dock = _dock};
            var waterTwo = new Water();
            var waterThree = new Water();
            var waterFour = new Water();
            var waterFive = new Water();

            waterOne.Next = waterTwo;
            waterTwo.Previous = waterOne;
            waterTwo.Next = waterThree;
            waterThree.Previous = waterTwo;
            waterThree.Next = waterFour;
            waterFour.Previous = waterThree;
            waterFour.Next = waterFive;
            waterFive.Previous = waterFour;

            return waterOne;
        }

        private static void GenerateSwitches()
        {
            SwitchType[] types = {SwitchType.Merge, SwitchType.Fork, SwitchType.Merge, SwitchType.Fork, SwitchType.Merge, };
            for (var i = 0; i < 5; i++)
            {
                _switches.Add(i +1, new Switch(i +1, types[i]));
            }
        }

        private static Yard GenerateYardLane()
        {
            var firstYard = new Yard();

            var currentYard = firstYard;
            for (var i = 0; i < 7; i++)
            {
                currentYard.Next = new Yard();
                currentYard = (Yard)currentYard.Next;
            }

            return firstYard;
        }

        private static void GenerateRoute()
        {
            var switchOne = _switches[1];
            var switchTwo = _switches[2];
            var switchThree = _switches[3];
            var switchFour = _switches[4];
            var switchFive = _switches[5];

            // Warehouse A
            var trackThree = new Track { Next = switchOne };
            var trackTwo = new Track { Next = trackThree };
            var trackOne = new Track { Next = trackTwo };

            // Warehouse B
            var trackSix = new Track { Next = switchOne };
            var trackFive = new Track { Next = trackSix };
            var trackFour = new Track { Next = trackFive };

            // Between Switch 1 & 2
            var trackSeven = new Track { Next = switchTwo };

            switchOne.Top = trackThree;
            switchOne.Bottom = trackSix;
            switchOne.Tail = trackSeven;

            // Warehouse C
            var trackFourteen = new Track { Next = switchThree };
            var trackThirteen = new Track { Next = trackFourteen };
            var trackTwelve = new Track { Next = trackThirteen };
            var trackEleven = new Track { Next = trackTwelve };
            var trackTen = new Track { Next = trackEleven };
            var trackNine = new Track { Next = trackTen };
            var trackEight = new Track{Next = trackNine};

            // Top after switch 2
            var trackNineteen = new Track { Next = switchFive };
            var trackEighteen = new Track { Next = trackNineteen };
            var trackSeventeen = new Track { Next = trackEighteen };
            var trackSixteen = new Track { Next = trackSeventeen };
            var trackFifteen = new Track { Next = trackSixteen };

            // Between switch 2 & 3
            var trackTwenty = new Track { Next = switchThree };

            switchTwo.Tail = trackSeven;
            switchTwo.Top = trackFifteen;
            switchTwo.Bottom = trackTwenty;

            // Between switch 3 & 4
            var trackTwentyOne = new Track { Next = switchFour };

            switchThree.Tail = trackTwentyOne;
            switchThree.Top = trackTwenty;
            switchThree.Bottom = trackFourteen;

            // Between switch 4 & 5
            var trackTwentyTwo = new Track { Next = switchFive };

            // Bottom after switch 4
            var trackTwentyThree = new Track();
            var trackThirtySix = trackTwentyThree;
            for (var i = 0; i < 12; i++)
            {
                trackThirtySix.Next = new Track();
                trackThirtySix = trackThirtySix.Next;
            }

            trackThirtySix.Next = GenerateYardLane();
            
            switchFour.Tail = trackTwentyOne;
            switchFour.Top = trackTwentyTwo;
            switchFour.Bottom = trackTwentyThree;

            // Top after switch 5
            var trackThirtySeven = new Track();
            var trackFiftyOne = trackThirtySeven;
            for (var i = 0; i < 18; i++)
            {
                if (i + 38 == 52)
                {
                    trackFiftyOne.Dock = _dock;
                }

                trackFiftyOne.Next = new Track();
                trackFiftyOne = trackFiftyOne.Next;
            }

            switchFive.Bottom = trackTwentyTwo;
            switchFive.Top = trackNineteen;
            switchFive.Tail = trackThirtySeven;

            _warehouses.Add('A', new Warehouse { FirstTrack = trackOne });
            _warehouses.Add('B', new Warehouse { FirstTrack = trackFour });
            _warehouses.Add('C', new Warehouse { FirstTrack = trackEight });
        }
    }
}