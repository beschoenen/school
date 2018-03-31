using System;

namespace ALGA
{
    public class Node
    {
        public Node left, right;

        public int number { get; }

        public Node(int number)
        {
            this.number = number;
        }

        public void setNext(int number)
        {
            var node = new Node(number);

            if (this.number > number)
            {
                left = node;
                return;
            }

            right = node;
        }

        public Node getNext(int number)
        {
            return this.number > number ? left : right;
        }

        public void insert(int number)
        {
            var nextNode = getNext(number);

            if (nextNode == null)
            {
                setNext(number);
                return;
            }

            nextNode.insert(number);
        }

        public void delete(int number, Node parent)
        {
            //
        }

        public bool exists(int number)
        {
            return this.number == number || (getNext(number)?.exists(number) ?? false);
        }

        public int min()
        {
            return left?.min() ?? number;
        }

        public int max()
        {
            return right?.max() ?? number;
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

        public int count()
        {
            if (left == null && right == null)
            {
                return 1;
            }

            if (left == null)
            {
                return right.count() + 1;
            }

            if (right == null)
            {
                return left.count() + 1;
            }

            return left.count() + right.count() + 1;
        }

        public void print()
        {
            print(null, null);
        }

        public void print(int? min, int? max)
        {
            if (min == null || number > min)
            {
                left?.print(min, max);
            }

            Console.Write($"{number}, ");

            if (max == null || number < max)
            {
                right?.print(min, max);
            }
        }
    }
}
