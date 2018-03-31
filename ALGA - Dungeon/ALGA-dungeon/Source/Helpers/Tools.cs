using System;

namespace ALGAdungeon.Source.Helpers
{
    public class Tools
    {
        public static int AskForNumber(string title)
        {
            var number = 0;

            Console.WriteLine(title);
            while (number < 3 || number > 25)
            {
                Console.Write("-> ");
                var input = Console.ReadLine();

                int.TryParse(input, out number);
            }

            return number;
        }

        public static string AskForInput()
        {
            Console.Write("-> ");
            return Console.ReadLine();
        }
    }
}