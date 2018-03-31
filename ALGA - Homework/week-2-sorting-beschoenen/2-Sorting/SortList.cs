using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALGA
{
    public interface ISortList
    {
        int Swaps { get; }
        int Comparisons { get; }
        int Gets { get; }
        int Sets { get; }
        int this[int index] { get; set; }

        int Count { get; }
        void swap(int i1, int i2);
        int compare(int i1, int i2);
    }

    public class SortList : ISortList
    {
        public int Swaps { get; private set; }
        public int Comparisons { get; private set; }
        public int Gets { get; private set; }
        public int Sets { get; private set; }

        public int Count {
            get
            {
                return list.Length;
            }
        }

        private int[] list;

        public SortList(params int[] input)
        {
            list = input;
            Swaps = 0;
            Comparisons = 0;
            Gets = 0;
            Sets = 0;
        }
        public SortList(int length, bool sorted=false, bool reverse=true, int seed = 1)
        {
            // Generate a list in ascending (or descending) order
            list = new int[length];
            for(int i = 0; i < length; i++)
            {
                list[i] = reverse ? length-i : i;
            }

            // Shuffle the list if it should not be sorted
            if (!sorted)
            {
                Random random = new Random(seed);
                int n = length;
                while (n > 1)
                {
                    n--;
                    int k = random.Next(n + 1);
                    swap(k, n);
                }
            }

            Swaps = 0;
            Comparisons = 0;
            Gets = 0;
            Sets = 0;
        }

        public int this[int index]
        {
            get
            {
                Gets++;
                return list[index];
            }
            set
            {
                Sets++;
                list[index] = value;
            }
        }

        /**
         * Swap the values at index i1 and i2 around
         */
        public void swap(int i1, int i2)
        {
            if(i1 < 0 || i1 >= Count)
            {
                throw new IndexOutOfRangeException(String.Format("First swap index ({0}) is out of bounds (0-{1})", i1, Count - 1));
            } else if(i2 < 0 || i2 >= Count)
            {
                throw new IndexOutOfRangeException(String.Format("Second swap index ({0}) is out of bounds (0-{1})", i1, Count - 1));
            }
            int temp = list[i1];
            list[i1] = list[i2];
            list[i2] = temp;

            Swaps++;
        }

        /**
         * Compare two values at index i1 and i2 with eachother
         * Returns a negative integer if the value at the first index is smaller than the value at the second index
         * Returns 0 if the value at the first index is equal to the value at the second index
         * Returns a positive integer if the value at the first index is greater that the value at the second index
         */
        public int compare(int i1, int i2)
        {
            if (i1 < 0 || i1 >= Count)
            {
                throw new IndexOutOfRangeException(String.Format("First compare index ({0}) is out of bounds (0-{1})", i1, Count - 1));
            }
            else if (i2 < 0 || i2 >= Count)
            {
                throw new IndexOutOfRangeException(String.Format("Second compare index ({0}) is out of bounds (0-{1})", i1, Count - 1));
            }
            Comparisons++;

            return list[i1] - list[i2];
        }

        public bool isSorted()
        {
            if(Count <= 1)
            {
                return true;
            }

            // If an element before a smaller element then the list is not sorted
            for(int i = 1; i < Count; i++)
            {
                if(list[i - 1] > list[i])
                {
                    return false;
                }
            }
            return true;
        }

        public bool isPartitioned(int pivotIndex)
        {
            int pivot = list[pivotIndex];
            for(int i = 0; i < pivotIndex; i++)
            {
                if(i < pivotIndex && list[i] > list[pivotIndex])
                {
                    return false;
                }
                if(i > pivotIndex && list[i] < list[pivotIndex])
                {
                    return false;
                }
            }
            return true;
        }

        public override string ToString()
        {
            return "[" + string.Join(", ", list) + "]";
        }
    }
}
