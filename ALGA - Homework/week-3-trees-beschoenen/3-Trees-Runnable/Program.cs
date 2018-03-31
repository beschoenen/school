using System;

namespace ALGA
{
    class Program
    {
        static void Main(string[] args)
        {
            /**
             *      10
             *     / \
             *    5   13
             *   / \   \
             *  3   7   16
             * /       /
             *1       15
             */
            var tree = new BinaryTree
            {
                root = new Node(10)
                {
                    left = new Node(5)
                    {
                        left = new Node(3)
                        {
                            left = new Node(1)
                        },
                        right = new Node(7)
                    },
                    right = new Node(13)
                    {
                        right = new Node(16)
                        {
                            left = new Node(15)
                        }
                    }
                }
            };

            tree.print(); // Should print: 1, 3, 5, 7, 10, 13, 15, 16

            Console.WriteLine();

            tree.printInRange(5, 13); // Should print: 5, 7, 10, 13

            Console.ReadLine();
        }
    }
}
