using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALGA
{

    public class Collatz
    {
        public static int collatz_recursive(int n)
        {
            if (n < 1) return -1;

            if (n == 1) return 0;

            var a = n;
            var steps = 1;

            if (a % 2 == 0)
            {
                a = a / 2;
            }
            else
            {
                a = a * 3 + 1;
            }

            if (a != 1)
            {
                steps += collatz_recursive(a);
            }

            return steps;
        }

        public static int collatz_iterative(int n)
        {
            if (n < 1) return -1;

            var a = n;
            var steps = 0;

            while (a != 1)
            {
                steps++;

                if (a % 2 == 0)
                {
                    a = a / 2;
                }
                else
                {
                    a = a * 3 + 1;
                }
            }

            return steps;
        }
    }
}
