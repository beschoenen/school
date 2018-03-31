using System;

namespace ALGA
{
    public class AVLTree
    {
        public Node root;

        public void insert(int number)
        {
            if (root == null)
            {
                root = new Node(number);
                return;
            }

            root = root.insert(number);
        }

        public bool isBalanced()
        {
            return root == null || root.isBalanced();
        }

        public void prettyprint()
        {
            root?.prettyprint("→", " ");
        }
    }
}
