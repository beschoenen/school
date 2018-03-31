using ALGA;
using NUnit.Framework;

namespace ALGA_test
{
    [Category("Pyramid Problem"), Timeout(1000)]
    public class PyramidProblemTest
    {
        [Test]
        public void PyramidProblemRecursiveNegative()
        {
            Assert.AreEqual(0, PyramidProblem.triangular_number_recursive(-4));
        }

        [Test]
        public void PyramidProblemRecursive()
        {
            Assert.AreEqual(0, PyramidProblem.triangular_number_recursive(0));
            Assert.AreEqual(1, PyramidProblem.triangular_number_recursive(1));
            Assert.AreEqual(3, PyramidProblem.triangular_number_recursive(2));
            Assert.AreEqual(6, PyramidProblem.triangular_number_recursive(3));
            Assert.AreEqual(10, PyramidProblem.triangular_number_recursive(4));
            Assert.AreEqual(15, PyramidProblem.triangular_number_recursive(5));
            Assert.AreEqual(21, PyramidProblem.triangular_number_recursive(6));
            Assert.AreEqual(28, PyramidProblem.triangular_number_recursive(7));
            Assert.AreEqual(36, PyramidProblem.triangular_number_recursive(8));
            Assert.AreEqual(45, PyramidProblem.triangular_number_recursive(9));
            Assert.AreEqual(55, PyramidProblem.triangular_number_recursive(10));
        }

        [Test]
        public void PyramidProblemIterativeNegative()
        {
            Assert.AreEqual(0, PyramidProblem.triangular_number_iterative(-4));
        }

        [Test]
        public void PyramidProblemIterative()
        {
            Assert.AreEqual(0, PyramidProblem.triangular_number_iterative(0));
            Assert.AreEqual(1, PyramidProblem.triangular_number_iterative(1));
            Assert.AreEqual(3, PyramidProblem.triangular_number_iterative(2));
            Assert.AreEqual(6, PyramidProblem.triangular_number_iterative(3));
            Assert.AreEqual(10, PyramidProblem.triangular_number_iterative(4));
            Assert.AreEqual(15, PyramidProblem.triangular_number_iterative(5));
            Assert.AreEqual(21, PyramidProblem.triangular_number_iterative(6));
            Assert.AreEqual(28, PyramidProblem.triangular_number_iterative(7));
            Assert.AreEqual(36, PyramidProblem.triangular_number_iterative(8));
            Assert.AreEqual(45, PyramidProblem.triangular_number_iterative(9));
            Assert.AreEqual(55, PyramidProblem.triangular_number_iterative(10));
        }

        [Test]
        public void PyramidProblemFunction()
        {
            Assert.AreEqual(0, PyramidProblem.triangular_number_function(0));
            Assert.AreEqual(1, PyramidProblem.triangular_number_function(1));
            Assert.AreEqual(3, PyramidProblem.triangular_number_function(2));
            Assert.AreEqual(6, PyramidProblem.triangular_number_function(3));
            Assert.AreEqual(10, PyramidProblem.triangular_number_function(4));
            Assert.AreEqual(15, PyramidProblem.triangular_number_function(5));
            Assert.AreEqual(21, PyramidProblem.triangular_number_function(6));
            Assert.AreEqual(28, PyramidProblem.triangular_number_function(7));
            Assert.AreEqual(36, PyramidProblem.triangular_number_function(8));
            Assert.AreEqual(45, PyramidProblem.triangular_number_function(9));
            Assert.AreEqual(55, PyramidProblem.triangular_number_function(10));
        }
    }
}
