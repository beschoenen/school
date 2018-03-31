using System;

namespace Sokoban.Views
{
	public class MapView : IViewInterface
	{
		public static void PrintView()
		{
			throw new System.NotImplementedException();
		}

	    void IViewInterface.PrintView()
	    {
	        PrintView();
	    }

	    public static void PrintView(string map)
        {
            Console.Clear();
	        Console.Write(
                "┌──────────┐\n" +
                "| Sokoban  |\n" +
                "└──────────┘\n" +
                "─────────────────────────────────────────────────────────────────────────\n" +
                    map +
                "─────────────────────────────────────────────────────────────────────────\n" +
                "> gebruik pijljestoetsen (s = stop, r = reset)\n"
            );
	    }
	}
}

