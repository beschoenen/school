using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALGA
{
    public class Fibonacci
    {
        public static int fibonacci_recursive(int n)
        {
            if (n == 0 || n == 1) return n;

            if (n < 0) return 0;

            return fibonacci_recursive(n - 1) + fibonacci_recursive(n - 2);
        }

        public static int fibonacci_iterative(int n)
        {
            var a = 0;
            var b = 1;

            for (var i = 0; i < n; i++)
            {
                var temp = a;
                a = b;
                b = temp + b;
            }

            return a;
        }

        public enum Answer { IterativeIsFaster, RecursiveIsFaster };

        public static Answer which_is_faster()
        {
            return Answer.IterativeIsFaster;
        }
    }
}
