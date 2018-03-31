using System;
using Gold_Rush.Model;
using Gold_Rush.View;

namespace Gold_Rush.Controller
{
    public class GameController
    {
        public Game Game;

        public GameController()
        {
            OutputView.PrintWelcomeMessage();
            InputView.PrintGameStart();

            Console.ReadLine();

            PlayGame();
        }

        private ConsoleKey AskInput()
        {
            return Console.ReadKey(true).Key;
        }

        public void PlayGame()
        {
            Game = new Game();

            while (Game.IsPlaying)
            {
                Game.MoveSwitch(AskInput());
            }
        }
    }
}