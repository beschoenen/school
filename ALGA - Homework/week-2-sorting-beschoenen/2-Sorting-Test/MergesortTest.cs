using System;
using NUnit.Framework;
using ALGA;

namespace ALGA_test
{
    [Category("Mergesort")]
    public class MergesortTest
    {
        [Test]
        public void MergesortOneElement()
        {
            SortList list = new SortList(1);
            Mergesort.mergesort(list);
        }

        [Test]
        public void MergesortTwoElements()
        {
            SortList list = new SortList(2);
            Mergesort.mergesort(list);
            Assert.IsTrue(list.isSorted());
        }

        [Test]
        public void MergesortThreeElements()
        {
            SortList list = new SortList(3);
            Mergesort.mergesort(list);
            Assert.IsTrue(list.isSorted());
        }

        [Test]
        public void MergesortFourElements()
        {
            SortList list = new SortList(4);
            Mergesort.mergesort(list);
            Assert.IsTrue(list.isSorted());
        }

        [Test]
        public void MergesortHundredElements()
        {
            SortList list = new SortList(100);
            Mergesort.mergesort(list);
            Assert.IsTrue(list.isSorted());
        }

        [Test]
        public void MergesortEfficiently()
        {
            SortList list = new SortList(100);
            Mergesort.mergesort(list);
            Assert.LessOrEqual(list.Gets, 700);
            Assert.LessOrEqual(list.Sets, 700);
        }
    }
}