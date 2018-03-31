using System.Collections;
using NUnit.Framework;
using ALGA;
using System.Collections.Generic;
using System;

namespace ALGA_test
{
    [Category("AVLTree")]
    public class BinaryTreeTest
    {
        private AVLTree empty_tree;
        private AVLTree only_root;
        private AVLTree two_nodes;
        private AVLTree three_nodes_unbalanced;
        private AVLTree big_tree_balanced;
        private AVLTree big_tree_unbalanced;

        [SetUp]
        public void Setup()
        {
            empty_tree = new AVLTree();

            /**
             * 0
             */
            only_root = new AVLTree();
            only_root.root = new Node(0);

            /**
             * 2
             *  \
             *   5
             */
            two_nodes = new AVLTree();
            two_nodes.root = new Node(2);
            two_nodes.root.right = new Node(5);

            /**
             *   10
             *  /
             * 5
             *  \
             *   7
             */
            three_nodes_unbalanced = new AVLTree();
            three_nodes_unbalanced.root = new Node(10);
            three_nodes_unbalanced.root.left = new Node(5);
            three_nodes_unbalanced.root.left.right = new Node(7);

            /**         8
             *        /   \
             *       5     11
             *      / \    / \
             *     3   7  10  12
             *    / \  /  /
             *   2  4 6  9
             *  /
             * 1
             */
            big_tree_balanced = new AVLTree();
            big_tree_balanced.root = new Node(8);
            big_tree_balanced.root.left = new Node(5);
            big_tree_balanced.root.left.left = new Node(3);
            big_tree_balanced.root.left.left.left = new Node(2);
            big_tree_balanced.root.left.left.left.left = new Node(1);
            big_tree_balanced.root.left.left.right = new Node(4);
            big_tree_balanced.root.left.right = new Node(7);
            big_tree_balanced.root.left.right.left = new Node(6);
            big_tree_balanced.root.right = new Node(11);
            big_tree_balanced.root.right.left = new Node(10);
            big_tree_balanced.root.right.left.left = new Node(9);
            big_tree_balanced.root.right.right = new Node(12);

            /**         8
             *        /   \
             *       5     11
             *      / \    / \
             *     3   7  10  12
             *    / \  /
             *   2  4 6
             *  /
             * 1
             */
            big_tree_unbalanced = new AVLTree();
            big_tree_unbalanced.root = new Node(8);
            big_tree_unbalanced.root.left = new Node(5);
            big_tree_unbalanced.root.left.left = new Node(3);
            big_tree_unbalanced.root.left.left.left = new Node(2);
            big_tree_unbalanced.root.left.left.left.left = new Node(1);
            big_tree_unbalanced.root.left.left.right = new Node(4);
            big_tree_unbalanced.root.left.right = new Node(7);
            big_tree_unbalanced.root.left.right.left = new Node(6);
            big_tree_unbalanced.root.right = new Node(11);
            big_tree_unbalanced.root.right.left = new Node(10);
            big_tree_unbalanced.root.right.right = new Node(12);
        }

        [Test]
        public void AVLTreeRotateLeft()
        {
            Node new_root = two_nodes.root.rotateLeft();

            /**
             *   5
             *  /
             * 2
             */
            Assert.AreEqual(5, new_root.number);
            Assert.AreEqual(2, new_root.left.number);
            Assert.IsNull(new_root.left.left);
            Assert.IsNull(new_root.left.right);
            Assert.IsNull(new_root.right);


            /**           11
             *           /  \
             *          8   12
             *        /   \
             *       5    10
             *      / \
             */
            new_root = big_tree_balanced.root.rotateLeft();
            Assert.AreEqual(11, new_root.number);
            Assert.AreEqual(8, new_root.left.number);
            Assert.AreEqual(12, new_root.right.number);
            Assert.AreEqual(5, new_root.left.left.number);
            Assert.AreEqual(10, new_root.left.right.number);
        }

        [Test]
        public void AVLTreeRotateRight()
        {
            /**  5
             *    \
             *     10
             *    /
             *   7
             */
            Node new_root = three_nodes_unbalanced.root.rotateRight();
            Assert.AreEqual(5, new_root.number);
            Assert.IsNull(new_root.left);
            Assert.AreEqual(10, new_root.right.number);
            Assert.AreEqual(7, new_root.right.left.number);
            Assert.IsNull(new_root.right.left.left);
            Assert.IsNull(new_root.right.left.right);
            Assert.IsNull(new_root.right.right);
        }

        [Test]
        public void AVLTreeInsert()
        {
            /**
             * 4
             */
            AVLTree one_insert = new AVLTree();
            one_insert.insert(4);
            Assert.AreEqual(4, one_insert.root.number);

            /**
             * 1
             *  \
             *   2
             */
            AVLTree two_inserts = new AVLTree();
            two_inserts.insert(1);
            two_inserts.insert(2);
            Assert.AreEqual(1, two_inserts.root.number);
            Assert.AreEqual(2, two_inserts.root.right.number);

            /**
             * The tree must have made a right
             * rotation to keep it AVL balanced:
             *   2
             *  / \
             * 1   3
             */
            AVLTree three_inserts = new AVLTree();
            three_inserts.insert(1);
            three_inserts.insert(2);
            three_inserts.insert(3);
            Assert.AreEqual(2, three_inserts.root.number);
            Assert.AreEqual(1, three_inserts.root.left.number);
            Assert.AreEqual(3, three_inserts.root.right.number);

            /**
             * Insert hundred ascending numbers
             */
            AVLTree hundred_inserts = new AVLTree();
            for(int i = 1; i <= 100; i++)
            {
                hundred_inserts.insert(i);
            }
            Assert.IsTrue(IsSorted(hundred_inserts.root));
            Assert.LessOrEqual(GetDepth(hundred_inserts), 7);
            
            /**
             * Insert hundred descending numbers
             */
            AVLTree hundred_inserts_descending = new AVLTree();
            for (int i = 100; i >= 1; i--)
            {
                hundred_inserts_descending.insert(i);
            }
            Assert.IsTrue(IsSorted(hundred_inserts_descending.root));
            Assert.LessOrEqual(GetDepth(hundred_inserts_descending), 7);

            /**
             * Insert hundred equal numbers
             */
            AVLTree hundred_inserts_equal = new AVLTree();
            for (int i = 0; i < 100; i++)
            {
                hundred_inserts_equal.insert(0);
            }
            Assert.IsTrue(IsSorted(hundred_inserts_equal.root));
            Assert.LessOrEqual(GetDepth(hundred_inserts_equal), 7);


            /**
             * Insert thousand random numbers
             */
            AVLTree thousand_inserts_random = new AVLTree();
            Random f = new Random();
            for (int i = 0; i < 1000; i++)
            {
                thousand_inserts_random.insert(f.Next(1, 1000));
            }
            Assert.IsTrue(IsSorted(thousand_inserts_random.root));
            Assert.LessOrEqual(GetDepth(thousand_inserts_random), 12);

        }

        /**
         * Iterative version of depth()
         * Do not use in your own solution!
         */
        private int GetDepth(AVLTree tree)
        {
            if(tree.root == null)
            {
                return 0;
            }
            var queue = new Queue<Node>();
            var depthForNode = new Dictionary<Node, int>();
            queue.Enqueue(tree.root);
            depthForNode[tree.root] = 1;
            int maxDepth = 1;

            while(queue.Count > 0)
            {
                Node node = queue.Dequeue();
                int depthOfNode = depthForNode[node];

                if(depthOfNode > maxDepth)
                {
                    maxDepth = depthOfNode;
                }
                
                if(node.left != null)
                {
                    depthForNode[node.left] = depthOfNode + 1;
                    queue.Enqueue(node.left);
                }
                if(node.right != null)
                {
                    depthForNode[node.right] = depthOfNode + 1;
                    queue.Enqueue(node.right);
                }
            }
            
            return maxDepth;
        }

        /**
         * Checks if the BST property holds that all nodes left are smaller
         * and all nodes right are larger
         */
        private bool IsSorted(Node node)
        {
            return IsSorted(node, int.MinValue, int.MaxValue);
        }

        private bool IsSorted(Node node, int min, int max)
        {
            if(node == null)
            {
                return true;
            }
            return node.number >= min
                 && node.number <= max
                 && IsSorted(node.left, min, node.number)
                 && IsSorted(node.right, node.number, max);
        }

        [Test]
        public void AVLTreeIsBalanced()
        {
            Assert.IsTrue(empty_tree.isBalanced());
            Assert.IsTrue(only_root.isBalanced());
            Assert.IsTrue(two_nodes.isBalanced());
            Assert.IsFalse(three_nodes_unbalanced.isBalanced());
            Assert.IsTrue(big_tree_balanced.isBalanced());
            Assert.IsFalse(big_tree_unbalanced.isBalanced());
        }
    }
}
