namespace ALGA
{
    public class BinaryTree
    {
        public Node root;

        /**
         * Inserts a number into the tree
         */
        public void insert(int number)
        {
            if (root == null)
            {
                root = new Node(number);
                return;
            }

            root.insert(number);
        }

        /**
         * Delete a number from the tree
         */
        public void delete(int number)
        {
            if (root == null) return;

            if (root.number == number)
            {
                if (root.left == null && root.right == null)
                {
                    root = null;
                }
                else
                {
                    var newRoot = new Node(0) { left = root };

                    deleteWithParent(number, root, newRoot);

                    root = newRoot.left;
                }
            }
            else
            {
                deleteWithParent(number, root, null);
            }
        }

        private void deleteWithParent(int number, Node current, Node parent)
        {
            if (number < current.number)
            {
                if (current.left != null)
                {
                    deleteWithParent(number, current.left, current);
                }

                return;
            }

            if (number > current.number)
            {
                if (current.right != null)
                {
                    deleteWithParent(number, current.right, current);
                }

                return;
            }

            if (current.left != null && current.right != null)
            {
                //current.number = current.right.number;
                deleteWithParent(current.number, current.right, current);
            }
            else if (parent?.left == current)
            {
                parent.left = current.left ?? current.right;
            }
            else if (parent?.right == current)
            {
                parent.right = current.left ?? current.right;
            }
        }

        public bool exists(int number)
        {
            return root?.exists(number) ?? false;
        }

        /**
         * Returns the smallest value in the tree (or -1 if tree is empty)
         */
        public int min()
        {
            if (root == null) return -1;

            return root.min();
        }

        /**
         * Returns the largest value in the tree (or -1 if tree is empty)
         */
        public int max()
        {
            if (root == null) return -1;

            return root.max();
        }

        /**
         * Returns how many levels deep the deepest level in the tree is
         * (the empty tree is 0 levels deep, the tree with only one root node is 1 deep)
         * @return
         */
        public int depth()
        {
            return root?.depth() ?? 0;
        }

        /**
         * Returns the amount of nodes in the tree
         * @return
         */
        public int count()
        {
            return root?.count() ?? 0;
        }

        public void print()
        {
            root.print();
        }

        public void printInRange(int min, int max)
        {
            root.print(min, max);
        }
    }
}
