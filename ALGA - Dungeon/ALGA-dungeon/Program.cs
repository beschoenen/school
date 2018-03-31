using System;
using ALGAdungeon.Source;
using ALGAdungeon.Source.Helpers;

namespace ALGAdungeon
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var width = Tools.AskForNumber("How wide do you want your map? (3-25)");
            var height = Tools.AskForNumber("How high do you want your map? (3-25)");

            var game = new Game(width, height);

            while (!game.Over)
            {
                Console.Clear();
                Console.WriteLine(game);

                var validAction = false;
                while (!validAction)
                {
                    var action = Tools.AskForInput();

                    if (action == "q")
                    {
                        game.Over = true;
                        break;
                    }

                    validAction = game.Play(action);
                }
            }

            Console.WriteLine(game.LastAction);
            Console.WriteLine("Game Over.");
        }
    }
}
