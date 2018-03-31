using ALGA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALGA
{
    class Program
    {
        static void Main(string[] args)
        {
            AVLTree tree = new AVLTree();

            for (int i = 0; i < 20; i++)
            {
                tree.insert(i);
            }
            tree.prettyprint();
            Console.Read();
        }
    }
}
