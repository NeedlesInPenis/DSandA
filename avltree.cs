/*
23rd April 2017
Notes:
Does not support duplicate value
Only rely on recursive functions
Completed on: 23rd April 2017
 */
using System;

namespace adt
{
    interface IAvlTree
    {
        // add
        void insert(int key, object data);
        
        // delete by key
        void delete(int key);

        // display (inorder display)
        void display();
    }   

    class Avltree : IAvlTree
    {
        protected Node root = null;
        protected Node getRoot() { return root; }

        public void insert(int key, object data)
        {
            root = insert(getRoot(), key, data);
        }

        private Node insert(Node node, int key, object data)
        {
            if(node == null)
            {
                node = new Node(key, data);
                return node;
            }

            Node temp = null;
            if(key < node.key)
            {
                temp = insert(node.left, key, data);
                node.left = temp;
            }
            else if(key > node.key)
            {
                temp = insert(node.right, key, data);
                node.right = temp;
            }
            else
            {
                // avoiding duplicates
                Console.WriteLine("[ERROR] {0} key already exists", key);
                Console.WriteLine("[ERROR] call update(key, new_data) if you want to update data");

                return node;
            }
            
            // updating height
            node.height = 1 + max(getHeight(node.left), getHeight(node.right));

            // get balance factor
            int bal = getBalance(node);

            // if node is not balance
            // check 4 cases

            // left left case
            if(bal > 1 && key < node.left.key)
                return rightRotate(node);
            
            // right right case
            if(bal < -1 && key > node.right.key)
                return leftRotate(node);

            // left right case
            if(bal > 1 && key > node.left.key)
            {
                node.left = leftRotate(node.left);
                return rightRotate(node);
            }

            // right left case
            if(bal < -1 && key < node.right.key) 
            {
                node.right = rightRotate(node.right);
                return leftRotate(node);
            }
            
            return node;
        }

        public void delete(int key)
        {
            root = delete(getRoot(), key);
        }

        private Node delete(Node node, int key)
        {
            if(node == null) return node;

            // delete node in bst way
            if(key < node.key)
                node.left  = delete(node.left, key);
            else if(key > node.key)
                node.right = delete(node.right, key);
            else
            {
                if(node.key != key)
                {
                    Console.WriteLine("[ERROR] ABORT! KEY NOT FOUND");
                    return node;
                }

                // delete Node
                
                // no children and one child
                if(node.left == null)
                    return node.right;
                if(node.right == null)
                    return node.left;
                
                // have 2 children
                node.key = getMinValueNode(node.right).key;
                node.right = delete(node.right, node.key);

            }
            // after deletion balancing tree

            if(node == null) return node;

            node.height = 1 + max(getHeight(node.left), getHeight(node.right));

            int bal = getBalance(node);

            // left left
            if(bal > 1 && getBalance(node.left) >= 0)
                return rightRotate(node);

            // left right
            if(bal > 1 && getBalance(node.left) < 0)
            {
                node.left = leftRotate(node.left);
                return rightRotate(node);
            }

            // right right
            if(bal < -1 && getBalance(node.right) <= 0)
                return leftRotate(node);

            // right left
            if(bal < -1 && getBalance(node.right) > 0)
            {
                node.right = rightRotate(node.right);
                return leftRotate(node);
            }

            return node;
        }

        public void display()
        {
            //inorder(getRoot());
            LevelOrderInLinesI();
            Console.WriteLine();
        }

        private void inorder(Node node)
        {
            if (node == null) return;

            inorder(node.left);
            Console.Write("{0} ", node.key);
            inorder(node.right);

        }

        private int getBalance(Node node)
        {
            if(node == null)
                return 0;
            
            return getHeight(node.left) - getHeight(node.right);
        }

        private int getHeight(Node node){ return node == null ? 0 : node.height; }

        private int max(int a, int b) { return a > b ? a : b; }

        // rotations
        private Node rightRotate(Node node)
        {
            Node x = node.left;
            Node temp = x.right;

            x.right = node;
            node.left = temp;

            node.height = max(getHeight(node.left), getHeight(node.right)) + 1;
            x.height = max(getHeight(x.left), getHeight(x.right)) + 1;

            return x;
        }

        private Node leftRotate(Node node)
        {
            Node x = node.right;
            Node temp = x.left;

            x.left = node;
            node.right = temp;

            node.height = max(getHeight(node.left), getHeight(node.right)) + 1;
            x.height = max(getHeight(x.left), getHeight(x.right)) + 1;

            return x;
        }

        protected void LevelOrderInLinesI()
        {
            Console.WriteLine("in level order");
            Node curr = getRoot();
            if(curr == null) return;

            int nCount = 0;

            cQueue q = new cQueue();

            q.enqueue(curr);

            while(1==1)
            {
                nCount = q.getSize();
                if(nCount == 0) break;

                while(nCount > 0)
                {
                    curr = (Node)q.dequeue();

                    Console.Write("{0} ", curr.key);

                    if(curr.left != null) q.enqueue(curr.left);
                    if(curr.right != null) q.enqueue(curr.right);

                    nCount--;
                }

                Console.WriteLine();
            }
        }

        private Node getMinValueNode(Node node)
        {
            Node curr = node;
            if(curr == null) return curr;

            while(curr.left != null)
                curr = curr.left;
            
            return curr;
        }

    }
}