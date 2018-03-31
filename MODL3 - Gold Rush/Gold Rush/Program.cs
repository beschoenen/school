using Gold_Rush.Controller;
using Gold_Rush.View;

namespace Gold_Rush
{
    internal class Program
    {
        private static bool _wantsToPlay = true;

        private static void Main(string[] args)
        {
            var gameController = new GameController();

//            OutputView.PrintWelcomeMessage();

            while (_wantsToPlay)
            {
                gameController.PlayGame();

                // TODO Do you want to play again?
            }
        }
    }
}