using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALGA;
using NUnit.Framework;

namespace _2_Sorting_Test
{
    [Category("Speed")]
    public class SpeedTest
    {
        [Test]
        public void BubbleSortShuffled()
        {
            var list = new SortList(1000);
            Bubblesort.bubblesort(list);

            TestContext.Out.WriteLine("Gets: " + list.Gets);
            TestContext.Out.WriteLine("Sets: " + list.Sets);
            TestContext.Out.WriteLine("Swaps: " + list.Swaps);
            TestContext.Out.WriteLine("Comparisons: " + list.Comparisons);
        }

        [Test]
        public void BubbleSortSorted()
        {
            var list = new SortList(1000, true);
            Bubblesort.bubblesort(list);

            TestContext.Out.WriteLine("Gets: " + list.Gets);
            TestContext.Out.WriteLine("Sets: " + list.Sets);
            TestContext.Out.WriteLine("Swaps: " + list.Swaps);
            TestContext.Out.WriteLine("Comparisons: " + list.Comparisons);
        }

        [Test]
        public void MergeSortShuffled()
        {
            var list = new SortList(1000);
            Mergesort.mergesort(list);

            TestContext.Out.WriteLine("Gets: " + list.Gets);
            TestContext.Out.WriteLine("Sets: " + list.Sets);
            TestContext.Out.WriteLine("Swaps: " + list.Swaps);
            TestContext.Out.WriteLine("Comparisons: " + list.Comparisons);
        }

        [Test]
        public void MergeSortSorted()
        {
            var list = new SortList(1000, true);
            Mergesort.mergesort(list);

            TestContext.Out.WriteLine("Gets: " + list.Gets);
            TestContext.Out.WriteLine("Sets: " + list.Sets);
            TestContext.Out.WriteLine("Swaps: " + list.Swaps);
            TestContext.Out.WriteLine("Comparisons: " + list.Comparisons);
        }

        [Test]
        public void QuickSortShuffled()
        {
            var list = new SortList(1000);
            Quicksort.quicksort(list);

            TestContext.Out.WriteLine("Gets: " + list.Gets);
            TestContext.Out.WriteLine("Sets: " + list.Sets);
            TestContext.Out.WriteLine("Swaps: " + list.Swaps);
            TestContext.Out.WriteLine("Comparisons: " + list.Comparisons);
        }

        [Test]
        public void QuickSortSorted()
        {
            var list = new SortList(1000, true);
            Quicksort.quicksort(list);

            TestContext.Out.WriteLine("Gets: " + list.Gets);
            TestContext.Out.WriteLine("Sets: " + list.Sets);
            TestContext.Out.WriteLine("Swaps: " + list.Swaps);
            TestContext.Out.WriteLine("Comparisons: " + list.Comparisons);
        }

        [Test]
        public void QuickSort3Shuffled()
        {
            var list = new SortList(1000);
            Quicksort.quicksort3(list);

            TestContext.Out.WriteLine("Gets: " + list.Gets);
            TestContext.Out.WriteLine("Sets: " + list.Sets);
            TestContext.Out.WriteLine("Swaps: " + list.Swaps);
            TestContext.Out.WriteLine("Comparisons: " + list.Comparisons);
        }

        [Test]
        public void QuickSort3Sorted()
        {
            var list = new SortList(1000, true);
            Quicksort.quicksort3(list);

            TestContext.Out.WriteLine("Gets: " + list.Gets);
            TestContext.Out.WriteLine("Sets: " + list.Sets);
            TestContext.Out.WriteLine("Swaps: " + list.Swaps);
            TestContext.Out.WriteLine("Comparisons: " + list.Comparisons);
        }
    }
}
