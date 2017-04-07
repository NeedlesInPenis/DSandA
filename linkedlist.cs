using System;

namespace adt 
{
    class Node
    {
        public Node node = null;
        public object data = null;
    }

    interface ILinkedList
    {
        int getLength();
        int countItems();
        bool isEmpty();

        // appending
        void Add(object data);
        // prepending
        void AddTop(object data);

    }

    class LinkedList: ILinkedList
    {
        // provide access to derived classes
        protected Node head = null;
        protected Node tail = null;
        protected int listLength = -1;

        public void Add(object data)
        {
            // new node to add
            Node node = new Node();
            node.data = data;
            node.next = null;

            //appending node 
            if(isEmpty())
            {
                // create a new list
                head = node;
                tail = head;
            }
            else
            {
                
                tail.next = node;
                tail = node;
            }

            listLength++;
        }

        public void AddTop(object data)
        {
            // if empty create new list
            if(isEmpty())
            {
                Add(data);
                return;
            }

            Node node = new Node();
            node.data = data;
            
            // adding at top
            node.next = head;
            head = node;

            listLength++;
        }

        public int getLength() { return listLength; }
        public int countItems() { return listLength + 1; }
        public bool isEmpty() { return countItems() <= 0 ? true: false; }
    }
}