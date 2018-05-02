using System;

namespace ADS { 
    class Test
    {
        static void Main(string []args)
        {
            ADSTree t = new ADSTree();

            t.insert(43);
            t.insert(18);
            t.insert(22);
            t.insert(9);
            t.insert(21);
            t.insert(6);
            t.insert(8);
            t.insert(20);
            t.insert(63);
            t.insert(50);

            t.printTree(TraverseOrder.InOrder);
        }
    }
    public enum TraverseOrder
    {
        InOrder,
        PreOrder,
        PostOrder
    }

    public class ADSTree
    {
        private ADSNode root;

        public sealed class ADSNode
        {
            public ADSNode left;
            public ADSNode right;
            public int key;
            public int cardinality;  //  Increment each time duplicates are added
            public int height;  // Height of this node
        }

        public ADSTree()
        {
            root = null;
        }

        // Inserts a node into the tree and maintains its balance
        public void insert(int value)
        {
            if (root == null)
                root = new ADSNode() { key = value };
            else
                root = insert(value, root);
        }
        private ADSNode insert(int value, ADSNode node)
        {
            if (node == null) return new ADSNode() { key = value };

            if (node.key == value)
                node.cardinality++;
            else if (node.key < value)
            {
                node.right = insert(value, node.right);

            }
            else
            {
                node.left = insert(value, node.left);
            }
            return updateHeight(node);

        }
        public ADSNode updateHeight(ADSNode node)
        {
            int left = (node.left == null) ? 0 : node.left.height + 1;
            int right = (node.right == null) ? 0 : node.right.height + 1;

            int height = Math.Max(left, right);
            node.height = height;

            int difference = Math.Abs(right - left);

            if (difference >= 2)
            {
                node = balance(node);
            }

            return node;

        }
        private ADSNode balance(ADSNode node)
        {
            bool first = checkType(node);
            bool second = (first) ? checkType(node.left) : checkType(node.right);

            if (first && second)
            {
                return rightRotation(node);
            }
            else if (!first && !second)
            {
                return rightRotation(node);
            }
            else if (first && !second)
            {
                node = leftRightRotation(node);
                return rightRotation(node);
            }
            else if (!first && second)
            {
                node = rightLeftRotation(node);
                return leftRotation(node);

            }
            return updateHeight(node);

        }

        private bool checkType(ADSNode node)
        {
            int left = (node.left == null) ? 0 : node.left.height + 1;
            int right = (node.right == null) ? 0 : node.right.height + 1;

            return left > right;

        }
        private ADSNode rightRotation(ADSNode node)
        {
            //should return newRoot
            ADSNode newRoot = node.left;
            ADSNode temp = (newRoot.right == null) ? null : newRoot.right;
            newRoot.right = node;

            node.left = temp;

            return newRoot;

        }

        private ADSNode leftRotation(ADSNode node)
        {
            ADSNode newRoot = node.right;
            ADSNode temp = (newRoot.left == null) ? null : newRoot.left;
            newRoot.left = node;

            node.right = temp;

            return newRoot;
        }

        private ADSNode leftRightRotation(ADSNode node)
        {
            ADSNode temp = node.left;

            ADSNode newRoot = node.left.right;
            temp.right = null;
            node.left = newRoot;
            newRoot.left = temp;

            return node;

        }
        private ADSNode rightLeftRotation(ADSNode node)
        {
            ADSNode temp = node.right;

            ADSNode newRoot = node.right.left;
            temp.left = null;
            node.right = newRoot;
            newRoot.right = temp;

            return node;
        }
        // Print the tree in a particular order
        public void TreePrinter(TraverseOrder traverse){

            if(traverse == TraverseOrder.InOrder){
                InOrderTraversal(root);
            }else if(traverse == TraverseOrder.PreOrder){
                PreOrderTraversal(root);
            }else{
                PostOrderTraversal(root);
            }
        }
        public static void PreOrderTraversal(ADSNode root)
        {
            if (root == null) return;

            Console.WriteLine(root.key); // process the root
            PreOrderTraversal(root.left);// process the left
            PreOrderTraversal(root.right);// process the right
        }

        public static void InOrderTraversal(ADSNode root)
        {
            if (root == null) return;

            InOrderTraversal(root.left);// process the left
            Console.WriteLine(root.key); // process the root
            InOrderTraversal(root.right);// process the right
        }

        public static void PostOrderTraversal(ADSNode root)
        {
            if (root == null) return;

            PostOrderTraversal(root.left);// process the left            
            PostOrderTraversal(root.right);// process the right
            Console.WriteLine(root.key); // process the root
        }
    }
          
}
