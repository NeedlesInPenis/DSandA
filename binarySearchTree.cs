/* NOT COMPLETED, still working on it
08th April 2017
Basic implementation of Binary Search Tree. 
Note:
=>No duplicate values are allowed in the tree.
=>For some functions both iterative and recursive versions are provided.

 */

using System;

namespace adt
{
    interface IBinarySearchTree
    {
        /* features
        deletion
        isBst
        height
        successor
        predecessor 
        isBalanced
        Level order print
        
        */
        void insert(int val);

        void InOrder();
        void PreOrder();
        void PostOrder();

    }

    class baseForBst
    {
        protected Node root = null;

        protected Node getRoot() { return root; }

        protected virtual Node getMin(Node node) { return null; }
        protected virtual Node getMax(Node node) { return null; }

        public bool useIterative = false;
    }
    class recursiveFunctions : baseForBst
    {
        protected Node insertR(Node node, int val)
        {
            if(node == null)
            {
                node = new Node(val);

                return node;
            }

            Node temp = null;
            if(val < node.val) 
            {
                temp = insertR(node.left, val);
                node.left = temp;
                temp.parent = node;
            }
                
            else if( val > node.val)
            {
                temp  = insertR(node.right, val);
                node.right = temp;
                temp.parent = node;
            }
            else
            {
                Console.WriteLine("[ERROR] insertR(Node,int): {0} already exists", val);
            }

            return node;
        }

         protected Node deleteR(Node node, int val)
        {
            if(node == null) return null;
            /*
                Case 1: leaf node
                Case 2: Have node in left
                Case 3: Have node in right
                Case 4: Have both children
             */

            // finding node to be deleted
            if( val < node.val ) node.left = deleteR(node.left, val);
            else if(val > node.val) node.right = deleteR(node.right, val);
            else
            {
                // node found. deleting it

                //case 1, 2 and 3 
                if(node.left == null)
                    return node.right;
                
                if(node.right == null)
                    return node.left;
                
                // case 4
                // replacing node value with the minimum value in right subtree
                node.val = getMin(node.right).val;
                // removing duplicate value from right subtree
                node.right = deleteR(node.right, node.val);
            }
            
            return node;
             
        }

        protected void InOrderR(Node node)
        {
            if(node == null ) return;

            InOrderR(node.left);
            Console.Write("{0} ", node.val);
            InOrderR(node.right);
        }

        protected void PreOrderR(Node node)
        {
            if(node == null) return;

            Console.Write("{0} ", node.val);
            PreOrderR(node.left);
            PreOrderR(node.right);
        }

        protected void PostOrderR(Node node)
        {
            if(node == null) return;

            
            PostOrderR(node.left);
            PostOrderR(node.right);
            Console.Write("{0} ", node.val);
        }

        protected Node findValR(Node node, int val)
        {
            if(node == null) return null;

            if(node.val == val)
                return node;
            
            Node temp = findValR(node.left, val);
            if(temp == null)
                temp = findValR(node.right, val);

            return temp;
        }

        protected bool valExistsR(Node node, int val)
        {
            if(node == null) return false;

            // using pre-order traversal
            if(node.val == val)
                return true;
            
            bool temp = valExistsR(node.left, val);
            if(temp == false)
                 temp = valExistsR(node.right, val);

            return temp;
        }

        protected Node getParentR(Node curr, Node node)
        {
            if(curr == null || node == null) return null;

            if(node.val == getRoot().val) return null;

            if( (curr.left  != null && curr.left.val  == node.val) ||
                (curr.right != null && curr.right.val == node.val) ) return curr;

            if(node.val < curr.val) return getParentR(curr.left, node);
            else return getParentR(curr.right, node);

        }

        protected Node getMinR(Node node)
        {
            if(node == null) return null;

            if(node.left == null) return node;

            return getMinR(node.left);

        }

        protected Node getMaxR(Node node)
        {
            if(node == null) return null;

            if(node.right == null) return node;

            return getMaxR(node.right);
        }

    }

    class iterativeFunctions: recursiveFunctions
    {
        protected void insertI(int val)
        {
            Node node = new Node(val);

            if(getRoot() == null)
            {
                root = node;
                return;
            }

            Node curr = getRoot();
            Node parent = null;

            while(true)
            {
                parent = curr;

                if(val < parent.val)
                {
                    //going left
                    curr = curr.left;

                    if(curr == null)
                    {
                        parent.left = node;
                        node.parent = parent;
                        return;
                    }
                }
                else if(val > parent.val)
                {
                    // going right
                    curr = curr.right;

                    if(curr == null)
                    {
                        parent.right = node;
                        node.parent = parent;
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("[ERROR] insertI(int): {0} already exists", val);
                    return;
                }

            } //while() END

        }

        protected void InOrderI()
        {
            // this require Stack data structure
        }

        protected void PreOrderI()
        {
            // requires stack data structure
        }

        protected void PostOrderI()
        {
            // requires stack data structure
        }

        protected Node findValI(int val)
        {
            if(getRoot() == null) return null;

            Node curr = getRoot();

            while(curr.val != val)
            {
                if(val < curr.val)
                    curr = curr.left;
                else
                    curr = curr.right;
                if(curr == null)
                    return null;
            }

            return curr;
        }

        protected bool valExistsI(int val)
        {
            if(getRoot() == null) return false;            
            
            Node curr = getRoot();

            // binary searching
            while( curr.val != val )
            {
                if(val < curr.val)
                    curr = curr.left;
                else
                    curr = curr.right;

                // value not found
                if(curr == null)
                    return false;
            }

            // value found
            return true;
        }

        protected Node getParentI(Node node)
        {
            if(getRoot() == null) return null;

            Node curr = getRoot();
            if(curr == node && curr.val == node.val) return null; // root does not have parent

            while(curr != null)
            {
                if( (curr.left  != null && curr.left.val  == node.val) || 
                    (curr.right != null && curr.right.val == node.val) ) return curr;

                if(node.val < curr.val) curr = curr.left;
                else curr = curr.right;

            }

            return null;
        }

         protected Node getMinI(Node node)
        {
            if(node == null) return null;
            Node curr = node;

            while(curr.left != null)
            {
                curr = curr.left;
            }

            return curr;
        }

        protected Node getMaxI(Node node)
        {
            if(node == null) return null;

            Node curr = node;

            while(curr.right != null)
            {
                curr = curr.right;
            }

            return curr;
        }
        
    }
    class BinarySearchTree : iterativeFunctions, IBinarySearchTree
    {

        public void insert(int val)
        {
            if(!useIterative) root = insertR(getRoot(), val); // recursive version
            else insertI(val); // iterative
        }


#region dislay orders: InOrder, PreOrder, PostOrder, LevelOrder
    // In order --------------
        public void InOrder()
        {
            // using recursive version
            Console.WriteLine("=== Inorder ===");
            InOrderR(getRoot());
            Console.WriteLine();
        }      

    // Pre order -------------
        public void PreOrder()
        {
            Console.WriteLine("=== Pre Order ===");
            // recursive version
            PreOrderR(getRoot());
            Console.WriteLine();
        }
        

    // Post order ------------
        public void PostOrder()
        {
            Console.WriteLine("=== Post Order ===");
            //using recursive
            PostOrderR(getRoot());
            Console.WriteLine();
        }

        

    // Level order -----------
        public void LevelOrder()
        {
            // requires queue data structure
            Console.WriteLine("[ERROR] LevelOrder(): Not implemented");
        }
#endregion

#region searching: findVal, valExists
    // Finval ------    
        private Node findVal(int val)
        {
            // iterative 
            return findValI(val);

            // recursive
            // return findValR(val);
        }        

        
    // valExists -------
        public bool valExists(int val)
        {
            if(!useIterative) return valExistsR(getRoot(), val);  // recursive version
            else return valExistsI(val); // iterative version
        }

#endregion

        private Node getParent(Node node)
        {
            if(!useIterative) return getParentR(getRoot(), node);   // recursive
            else return getParentI(node); // iterative 
        }

        public int getMin()
        {
            Node temp = getMin(getRoot());
            return temp != null ? temp.val : -1;
        }

        public int getMax()
        {
            Node temp = getMax(getRoot());
            return temp != null ? temp.val : -1;
        }

        protected override Node getMin(Node node)
        {
            if(!useIterative) return getMinR(node); // recursive
            else return getMinI(node); // iterative
        }
        protected override Node getMax(Node node)
        {
            if(!useIterative) return getMaxR(node); // recursive
            else return getMaxI(node); // iterative
        }

        
        public void delete(int val)
        {
            if(!useIterative) root = deleteR(getRoot(), val);
        }

        protected void deleteI(int val)
        {

        }

       

    }   
}