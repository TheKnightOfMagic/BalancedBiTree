using System;

namespace ADS { 
    class Test
    {
        static void Main(string []args)
        {
            ADSTree t = new ADSTree();

            t.insert(1);
            t.insert(2);
            t.insert(3);
            t.insert(-1);
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
            public int height;
        }

        public ADSTree()
        {
        }
       
        // Return the node where value is located
        public ADSNode find(int value)
        {
            //use this to find the ending nodes The final children blah b kaajsdj
            int counter = 0;
            ADSNode current = new ADSNode();
            current = root;
            while (current != null)
            {
                if (value > current.key)
                {
                    current = current.right;
                }
                else
                {
                    current = current.left;
                }
            }
            return current;
        }
        public ADSNode unbalanceChecker(ADSNode current)
        {

            if (current.left == null || current.right == null)
            {
                int difference = Math.Abs(current.left.height - current.right.height);
                if (difference >= 2)
                {
                    return current;

                }
                else
                {
                    return null;
                }
            }
            return null;
        }
        public void updateHeight(ADSNode root)
        {
            if (current.left != null)
            {

            }

        }

        // Inserts a node into the tree and maintains its balance
        public void insert(int value)
        {
            string inbalanceType = "";
            bool imbalance = false;

            ADSNode node = new ADSNode() { key = value };
            if (root == null)
            {
                root = node;
                return;
            }

           
            ADSNode current = root;
            ADSNode parent;

            ADSNode unbalancedNode = null;
            //inserts node
            while (true)
            {
                parent = current;

                if (current.key > value)
                {
                    if (current.right == null && current.left == null) {
                        current.height++;
                    }
                    current = current.left;
                    //unbalancedNode = unbalanceChecker(parent);

                    if (current == null)
                    {
                        parent.left = node;
                        break;
                    }
                }
                else
                {
                    if (current.right == null && current.left == null) {
                        current.height++;
                    }
                    
                    current = current.right;
                    //unbalancedNode = unbalanceChecker(parent);

                    if (current == null)
                    {
                        parent.right = node;
                        break;
                    }
                }
            }
            updateHeight(root);
            //Find inbalance type
            if (unbalancedNode != null) {
                for (int i = 0; i < 2; i++) {
                    current = unbalancedNode;


                    if (current.right.height > current.left.height)
                    {
                        current = current.right;
                        inbalanceType += "R";
                    }
                    else
                    {
                        current = current.left;
                        inbalanceType += "L";
                    }

                }
                balanceIt(inbalanceType, unbalancedNode);
            }
        }
        public void balanceIt(string inbalanceType, ADSNode unbalancedNode)
        {
            ADSNode newRoot;
            ADSNode temp;
            if (inbalanceType == "LR")
            {
                newRoot = unbalancedNode.left.right;
                // :::
                newRoot.height += 2;
                newRoot.left = unbalancedNode.left;
                unbalancedNode.left = null;
                unbalancedNode.left.right = null;
                temp = unbalancedNode;
                newRoot.right = temp;
                newRoot.right.height -= 1;
                unbalancedNode = newRoot; 


            }else if(inbalanceType == "RL"){
                newRoot = unbalancedNode.right.left;
                newRoot.height += 2;

                newRoot.right = unbalancedNode.right;
                unbalancedNode.right = null;
                temp = unbalancedNode;
                newRoot.left = temp;
                newRoot.left.height -= 1;
                unbalancedNode = newRoot;




            }else if (inbalanceType == "RR")
            {
                ADSNode over = unbalancedNode.right.left;
                unbalancedNode.right.left = unbalancedNode;
                unbalancedNode.right = over;
                


            }
            else
            {
                newRoot = unbalancedNode.left;
                newRoot.height += 1;
                newRoot.left.height += 1;
                temp = unbalancedNode;
                while (newRoot.right != null)
                {
                    newRoot = newRoot.right;
                }
                newRoot.right = temp;
                newRoot.right.height -= 1;
                unbalancedNode = newRoot;
            }
        }
          


      
        // Print the tree in a particular order
        public void printTree(TraverseOrder order)
        {
            //in order is basically using the tree to print out everything in order: 6, 8, 9, 18, 20, 21, 22, 43, 50, 63

            /**
             
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

             * **/

            ADSNode current = root;
            ADSNode last;
            while (current.left != null)
            {
              
                    Console.WriteLine(current.key);
                    current = current.left;
                

            }

        }
    }
}
