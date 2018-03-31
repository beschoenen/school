using System;

namespace ALGA
{
    public class Node
    {
        public int number;

        public Node left, right;

        public Node(int number)
        {
            this.number = number;
        }

        public Node rotateLeft()
        {
            if (right == null) return this;

            var oldRight = right;

            right = right.left;

            oldRight.left = this;

            return oldRight;
        }

        public Node rotateRight()
        {
            if (left == null) return this;

            var oldLeft = left;

            left = left.right;

            oldLeft.right = this;

            return oldLeft;
        }

        public Node insert(int number)
        {
            if (this.number > number)
            {
                left = left == null ? new Node(number) : left.insert(number);
            }
            else if (this.number <= number)
            {
                right = right == null ? new Node(number) : right.insert(number);
            }

            return balance();
        }

        public bool isBalanced()
        {
            if (left == null && right == null)
            {
                return true;
            }

            if (left == null)
            {
                return right.depth() <= 1;
            }

            if (right == null)
            {
                return left.depth() <= 1;
            }

            return Math.Abs(left.depth() - right.depth()) <= 1;
        }

        public Node balance()
        {
            if (isBalanced()) return this;

            return (left?.depth() ?? 0) > (right?.depth() ?? 0) ? rotateRight() : rotateLeft();
        }

        public int depth()
        {
            if (left == null && right == null)
            {
                return 1;
            }

            if (left == null)
            {
                return right.depth() + 1;
            }

            if (right == null)
            {
                return left.depth() + 1;
            }

            return Math.Max(left.depth(), right.depth()) + 1;
        }

        public void prettyprint(String firstPrefix, String prefix)
        {
            Console.WriteLine(firstPrefix + number);

            if (right == null)
            {
                Console.WriteLine(prefix + "├── .");
            }
            else
            {
                right.prettyprint(prefix + "├── ", prefix + "|   ");
            }

            if (left == null)
            {
                Console.WriteLine(prefix + "└── .");
            }
            else
            {
                left.prettyprint(prefix + "└── ", prefix + "    ");
            }
        }
    }
}
