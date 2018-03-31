using System;
using NUnit.Framework;
using ALGA;

namespace ALGA_test
{
    [Category("Quicksort")]
    public class QuicksortTest
    {
        [Test]
        public void PartitionOneElement()
        {
            SortList list = new SortList(1);
            Assert.AreEqual(0, Quicksort.partition(list, 0, 0, 0));
        }

        [Test]
        public void PartitionTwoElements()
        {
            SortList list = new SortList(2, 1);
            Assert.AreEqual(1, Quicksort.partition(list, 0, 0, 1));
            Assert.IsTrue(list.isSorted());
        }

        [Test]
        public void PartitionThreeElements()
        {
            SortList list = new SortList(2, 3, 1);
            Assert.AreEqual(1, Quicksort.partition(list, 0, 0, 2));
            Assert.IsTrue(list.isSorted());
        }
        
        [Test]
        public void PartitionNineElements()
        {
            SortList list = new SortList(9, 5, 3, 1, 2, 6, 7, 8, 4);
            Assert.AreEqual(4, Quicksort.partition(list, 1, 0, 8)); // Partition on the 5
            Assert.AreEqual(5, list[4]);
            Assert.IsTrue(list.isPartitioned(4));
        }

        [Test]
        public void PartitionNineElementsHighPivot()
        {
            SortList list = new SortList(9, 5, 3, 1, 2, 6, 7, 8, 4);
            Assert.AreEqual(8, Quicksort.partition(list, 0, 0, 8)); // Partition on the 9
            Assert.AreEqual(9, list[8]);
            Assert.IsTrue(list.isPartitioned(8));
        }

        [Test]
        public void PartitionNineElementsLowPivot()
        {
            SortList list = new SortList(9, 5, 3, 1, 2, 6, 7, 8, 4);
            Assert.AreEqual(0, Quicksort.partition(list, 3, 0, 8)); // Partition on the 1
            Assert.AreEqual(1, list[0]);
            Assert.IsTrue(list.isPartitioned(0));
        }

        [Test]
        public void QuicksortOneElement()
        {
            SortList list = new SortList(1);
            Quicksort.quicksort(list);
        }

        [Test]
        public void QuicksortTwoElements()
        {
            SortList list = new SortList(2);
            Quicksort.quicksort(list);
            Assert.IsTrue(list.isSorted());
        }

        [Test]
        public void QuicksortThreeElements()
        {
            SortList list = new SortList(3);
            Quicksort.quicksort(list);
            Assert.IsTrue(list.isSorted());
        }

        [Test]
        public void QuicksortFourElements()
        {
            SortList list = new SortList(4);
            Quicksort.quicksort(list);
            Assert.IsTrue(list.isSorted());
        }

        [Test]
        public void QuicksortHundredElements()
        {
            SortList list = new SortList(100);
            Quicksort.quicksort(list);
            Assert.IsTrue(list.isSorted());
        }

        [Test]
        public void QuicksortALotOfElements()
        {
            SortList list = new SortList(317);
            Quicksort.quicksort(list);
            Assert.IsTrue(list.isSorted());
        }

        [Test]
        public void QuicksortSortedInput()
        {
            SortList list = new SortList(100, true);
            Quicksort.quicksort(list);
            Assert.IsTrue(list.isSorted());
            Assert.LessOrEqual(list.Comparisons, 5000);
            Assert.LessOrEqual(list.Swaps, 200);
        }

        [Test]
        public void QuicksortReverseSortedInput()
        {
            SortList list = new SortList(100, true, true);
            Quicksort.quicksort(list);
            Assert.IsTrue(list.isSorted());
        }

        [Test]
        public void QuicksortSortedInputEfficient()
        {
            SortList list = new SortList(100, true, true);
            Quicksort.quicksort(list);
            Assert.LessOrEqual(list.Comparisons, 5000);
            Assert.LessOrEqual(list.Swaps, 200);
        }

        [Test]
        public void QuicksortReverseSortedInputEfficient()
        {
            SortList list = new SortList(100, true, true);
            Quicksort.quicksort(list);
            Assert.LessOrEqual(list.Comparisons, 5000);
            Assert.LessOrEqual(list.Swaps, 200);
        }

        [Test]
        public void QuicksortNoDirectAccess()
        {
            SortList list = new SortList(100);
            Quicksort.quicksort(list);
            Assert.AreEqual(0, list.Gets, "Quicksort is an in-place algorithm, use only compare() and swap(). The [] getter is reserved for mergesort.");
            Assert.AreEqual(0, list.Sets, "Quicksort is an in-place algorithm, use only compare() and swap(). The [] setter is reserved for mergesort.");
        }


    }
}