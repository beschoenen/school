namespace ALGA
{
    public class Quicksort
    {
        public static void quicksort(ISortList list)
        {
            if (list == null || list.Count <= 1) return;

            quicksort(list, 0, list.Count - 1);
        }

        private static void quicksort(ISortList list, int leftIndex, int rightIndex)
        {
            if (leftIndex >= rightIndex) return;

            var pivot = partition(list, leftIndex + 1, leftIndex, rightIndex);

            if (pivot > 1)
            {
                quicksort(list, leftIndex, pivot - 1);
            }

            if (pivot + 1 < rightIndex)
            {
                quicksort(list, pivot + 1, rightIndex);
            }
        }

        public static void quicksort3(ISortList list)
        {
            if (list == null || list.Count <= 1) return;

            quicksort3(list, 0, list.Count - 1);
        }

        private static void quicksort3(ISortList list, int leftIndex, int rightIndex)
        {
            if (leftIndex >= rightIndex) return;

            var pivotIndex = leftIndex + 1;

            if (list.compare(leftIndex, rightIndex) > 0)
            {
                if (list.compare(leftIndex, leftIndex / rightIndex) > 0)
                {
                    pivotIndex = leftIndex;
                }
                else
                {
                    pivotIndex = leftIndex / rightIndex;
                }
            }
            else
            {
                if (list.compare(rightIndex, leftIndex / rightIndex) > 0)
                {
                    pivotIndex = rightIndex;
                }
                else
                {
                    pivotIndex = leftIndex / rightIndex;
                }
            }

            var pivot = partition(list, pivotIndex, leftIndex, rightIndex);

            if (pivot > 1)
            {
                quicksort3(list, leftIndex, pivot - 1);
            }

            if (pivot + 1 < rightIndex)
            {
                quicksort3(list, pivot + 1, rightIndex);
            }
        }

        /**
         * Partition the list according to the value at the pivot index
         * This method should only partition the part of the array that is between startIndex and endIndex
         * All values lower than the pivot value should be to the left of the pivot value
         * and all values higher than the pivot value should be to the right of the pivot value
         * 
         * This method should return the position of the pivot value after partitioning is complete
         * 
         * For example: partition([4, 9, 5, 0, 2], 2, 0, 4)
         * should partition the values between indices 0...4 (inclusive) using the value 5 as the pivot
         * A possible partitioning would be: [2, 0, 4, 5, 9], this method should return 3 as that is where
         * the pivot value 5 has ended up.
         * This method may assume there are no equal values
         */
        public static int partition(ISortList list, int pivotIndex, int leftIndex, int rightIndex)
        {
            if (leftIndex == rightIndex || list.Count <= 1) return 0; // Nothing to swap

            while (true)
            {
                while (list.compare(leftIndex, pivotIndex) < 0)
                {
                    leftIndex++;
                }

                while (list.compare(rightIndex, pivotIndex) > 0)
                {
                    rightIndex--;
                }

                if (leftIndex < rightIndex)
                {
                    if (pivotIndex == leftIndex)
                    {
                        pivotIndex = rightIndex;
                    }
                    else if (pivotIndex == rightIndex)
                    {
                        pivotIndex = leftIndex;
                    }

                    list.swap(rightIndex, leftIndex);
                }
                else
                {
                    return rightIndex;
                }
            }
        }
    }
}
