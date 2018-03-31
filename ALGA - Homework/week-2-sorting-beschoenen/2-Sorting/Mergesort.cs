using System.Collections.Generic;

namespace ALGA
{
    public class Mergesort
    {
        public static void mergesort(ISortList list)
        {
            mergesort(list, 0, list.Count - 1);
        }
        public static void mergesort(ISortList list, int leftIndex, int rightIndex)
        {
            // 1 item can't be sorted
            if (rightIndex - leftIndex == 0) return;

            // 2 items can be swapped
            if (rightIndex - leftIndex == 1)
            {
                // If left is bigger than right, swap
                if (list.compare(leftIndex, rightIndex) > 0)
                {
                    list.swap(leftIndex, rightIndex);
                }

                return;
            }

            var half = (leftIndex + rightIndex - 1) / 2;

            // More than 2 items have to be split
            if (rightIndex - leftIndex >= 2)
            {
                mergesort(list, leftIndex, half);
                mergesort(list, half + 1, rightIndex);
            }

            // Merge

            var currentLeft = leftIndex;
            var currentRight = half + 1;

            var tempList = new List<int>();

            while(currentLeft <= half || currentRight <= rightIndex)
            {
                if (currentLeft <= half && currentRight > rightIndex)
                {
                    tempList.Add(list[currentLeft]);
                    currentLeft++;
                }
                else if (currentRight <= rightIndex && currentLeft > half)
                {
                    tempList.Add(list[currentRight]);
                    currentRight++;
                }
                else if (list.compare(currentLeft, currentRight) < 0)
                {
                    tempList.Add(list[currentLeft]);
                    currentLeft++;
                }
                else
                {
                    tempList.Add(list[currentRight]);
                    currentRight++;
                }
            }

            // replace the items in the list with the items in the templist
            for (var i = 0; i < tempList.Count; i++)
            {
                list[i + leftIndex] = tempList[i];
            }
        }
    }
}
