using ALGA;
using NUnit.Framework;

namespace ALGA_test
{
    [Category("Collatz"), Timeout(1000)]
    public class CollatzTest
    {
        [Test]
        public void CollatzRecursiveNegative()
        {
            Assert.AreEqual(-1, Collatz.collatz_recursive(0));
            Assert.AreEqual(-1, Collatz.collatz_recursive(-1));
            Assert.AreEqual(-1, Collatz.collatz_recursive(-5));
        }

        [Test]
        public void CollatzRecursive()
        {
            Assert.AreEqual(0, Collatz.collatz_recursive(1));
            Assert.AreEqual(1, Collatz.collatz_recursive(2));
            Assert.AreEqual(2, Collatz.collatz_recursive(4));
            Assert.AreEqual(3, Collatz.collatz_recursive(8));
            Assert.AreEqual(4, Collatz.collatz_recursive(16));

            Assert.AreEqual(5, Collatz.collatz_recursive(5));
            Assert.AreEqual(5, Collatz.collatz_recursive(32));

            Assert.AreEqual(6, Collatz.collatz_recursive(10));
            Assert.AreEqual(6, Collatz.collatz_recursive(64));

            Assert.AreEqual(7, Collatz.collatz_recursive(3));
            Assert.AreEqual(7, Collatz.collatz_recursive(20));
            Assert.AreEqual(7, Collatz.collatz_recursive(21));
            Assert.AreEqual(7, Collatz.collatz_recursive(128));
        }

        [Test]
        public void CollatzIterativeNegative()
        {
            Assert.AreEqual(-1, Collatz.collatz_iterative(0));
            Assert.AreEqual(-1, Collatz.collatz_iterative(-1));
            Assert.AreEqual(-1, Collatz.collatz_iterative(-5));
        }

        [Test]
        public void CollatzIterative()
        {
            Assert.AreEqual(0, Collatz.collatz_iterative(1));
            Assert.AreEqual(1, Collatz.collatz_iterative(2));
            Assert.AreEqual(2, Collatz.collatz_iterative(4));
            Assert.AreEqual(3, Collatz.collatz_iterative(8));
            Assert.AreEqual(4, Collatz.collatz_iterative(16));

            Assert.AreEqual(5, Collatz.collatz_iterative(5));
            Assert.AreEqual(5, Collatz.collatz_iterative(32));

            Assert.AreEqual(6, Collatz.collatz_iterative(10));
            Assert.AreEqual(6, Collatz.collatz_iterative(64));

            Assert.AreEqual(7, Collatz.collatz_iterative(3));
            Assert.AreEqual(7, Collatz.collatz_iterative(20));
            Assert.AreEqual(7, Collatz.collatz_iterative(21));
            Assert.AreEqual(7, Collatz.collatz_iterative(128));
        }
    }
}

