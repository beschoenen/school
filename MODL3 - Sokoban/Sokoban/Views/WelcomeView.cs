using System;

namespace Sokoban.Views
{
	public class WelcomeView : IViewInterface
	{
		public static void PrintView()
        {
            Console.Clear();
		    Console.WriteLine(
		        "┌────────────────────────────────────────────────────┐\n" +
		        "| Welkom bij Sokoban                                 |\n" +
		        "|                                                    |\n" +
		        "| betekenis van de symbolen   |   doel van het spel  |\n" +
		        "|                             |                      |\n" +
		        "| spatie : outerspace         |   duw met de aap     |\n" +
		        "|      █ : muur               |   de banaan(en)      |\n" +
		        "|      · : vloer              |   naar de kisten     |\n" +
		        "|      O : banaan             |                      |\n" +
		        "|      0 : banaan op kist     |                      |\n" +
		        "|      x : kist               |                      |\n" +
		        "|      @ : aap                |                      |\n" +
		        "└────────────────────────────────────────────────────┘\n" +
		        "\n" +
		        "> Kies een doolhof (1 - 4), s = stop\n"
		    );

		}

	    void IViewInterface.PrintView()
	    {
	        PrintView();
	    }
	}
}

