using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sokoban.Models;
using Sokoban.Views;

namespace Sokoban.Controllers
{
    class GameController
    {
        private Game Game;
        private bool WantsToPlay = true;

        public GameController()
        {
            while (WantsToPlay)
            {
                WelcomeView.PrintView();
                var input = AskInput();

                var mazeKeys = new ConsoleKey[] {ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4};

                if (input == ConsoleKey.S)
                {
                    WantsToPlay = false;
                    break;
                }
                
                if (mazeKeys.Contains(input))
                {
                    PlayGame(Array.IndexOf(mazeKeys, input) + 1);
                }
            }
        }

        private ConsoleKey AskInput()
        {
            return Console.ReadKey(true).Key;
        }

        private void PlayGame(int i)
        {
            Game = new Game();
            var loaded = Game.LoadMaze(i);

            if (!loaded) return;

            var arrows = new[] {ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow};
            
            while (Game.IsPlaying)
            {
                MapView.PrintView(Game.GetMap());
                var input = AskInput();

                if (input == ConsoleKey.S) return;

                if (input == ConsoleKey.R)
                {
                    Game.LoadMaze(i);
                }

                if (arrows.Contains(input))
                {
                    Game.MoveMonkey(input);
                }
            }

            ResultView.PrintView();
            Console.ReadKey();
        }
    }
}
