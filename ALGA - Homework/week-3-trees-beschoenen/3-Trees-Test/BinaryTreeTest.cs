using System;
using NUnit.Framework;
using ALGA;

namespace ALGA_test
{
    [Category("BinaryTree")]
    public class BinaryTreeTest
    {
        private BinaryTree empty_tree;

        private BinaryTree only_root_tree;

        private BinaryTree three_nodes_tree;

        private BinaryTree big_tree;


        [SetUp]
        public void Setup()
        {
            empty_tree = new BinaryTree();

            only_root_tree = new BinaryTree();
            only_root_tree.root = new Node(5);

            /**
             *  5
             * / \
             *3   7 
             */
            three_nodes_tree = new BinaryTree();
            three_nodes_tree.root = new Node(5);
            three_nodes_tree.root.left = new Node(3);
            three_nodes_tree.root.right = new Node(7);

            /**
             *      10
             *     / \
             *    5   13
             *   / \   \
             *  3   7   16
             * /       /
             *1       15
             */
            big_tree = new BinaryTree();
            big_tree.root = new Node(10);
            big_tree.root.left = new Node(5);
            big_tree.root.left.left = new Node(3);
            big_tree.root.left.right = new Node(7);
            big_tree.root.left.left.left = new Node(1);
            big_tree.root.right = new Node(13);
            big_tree.root.right.right = new Node(16);
            big_tree.root.right.right.left = new Node(15);
        }

        [Test]
        public void BinaryTreeInsert()
        {
            empty_tree.insert(3);
            Assert.AreEqual(3, empty_tree.root.number);
            Assert.IsNull(empty_tree.root.left);
            Assert.IsNull(empty_tree.root.right);

            only_root_tree.insert(0);
            only_root_tree.insert(10);
            Assert.AreEqual(0, only_root_tree.root.left.number);
            Assert.AreEqual(10, only_root_tree.root.right.number);

            big_tree.insert(14);
            big_tree.insert(8);
            Assert.AreEqual(8, big_tree.root.left.right.right.number);
            Assert.AreEqual(14, big_tree.root.right.right.left.left.number);
        }

        [Test]
        public void BinaryTreeDelete()
        {
            empty_tree.delete(5);

            only_root_tree.delete(5);
            Assert.IsNull(only_root_tree.root);

            three_nodes_tree.delete(5);
            if (three_nodes_tree.root.number == 7)
            {
                Assert.AreEqual(3, three_nodes_tree.root.left.number);
                Assert.IsNull(three_nodes_tree.root.right);
            }
            else if (three_nodes_tree.root.number == 3)
            {
                Assert.AreEqual(7, three_nodes_tree.root.right.number);
                Assert.IsNull(three_nodes_tree.root.left);
            }
            else
            {
                Assert.Fail();
            }

            big_tree.delete(7);
            big_tree.delete(16);
            Assert.IsNull(big_tree.root.left.right);
            Assert.AreEqual(15, big_tree.root.right.right.number);
        }

        [Test]
        public void BinaryTreeExists()
        {
            Assert.IsFalse(empty_tree.exists(6));

            Assert.IsFalse(only_root_tree.exists(7));
            Assert.IsTrue(only_root_tree.exists(5));

            Assert.IsFalse(big_tree.exists(14));
            Assert.IsTrue(big_tree.exists(15));
            Assert.IsTrue(big_tree.exists(16));
            Assert.IsTrue(big_tree.exists(1));
        }

        [Test]
        public void BinaryTreeMin()
        {
            Assert.AreEqual(-1, empty_tree.min());
            Assert.AreEqual(5, only_root_tree.min());
            Assert.AreEqual(3, three_nodes_tree.min());
            Assert.AreEqual(1, big_tree.min());
        }

        [Test]
        public void BinaryTreeMax()
        {
            Assert.AreEqual(-1, empty_tree.max());
            Assert.AreEqual(5, only_root_tree.max());
            Assert.AreEqual(7, three_nodes_tree.max());
            Assert.AreEqual(16, big_tree.max());
        }

        [Test]
        public void BinaryTreeDepth()
        {
            Assert.AreEqual(0, empty_tree.depth());
            Assert.AreEqual(1, only_root_tree.depth());
            Assert.AreEqual(2, three_nodes_tree.depth());
            Assert.AreEqual(4, big_tree.depth());
        }

        [Test]
        public void BinaryTreeCount()
        {
            Assert.AreEqual(0, empty_tree.count());
            Assert.AreEqual(1, only_root_tree.count());
            Assert.AreEqual(3, three_nodes_tree.count());
            Assert.AreEqual(8, big_tree.count());
        }
    }
}
