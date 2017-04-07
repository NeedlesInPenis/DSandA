using System;

namespace adt 
{
    class Node
    {
        public Node next = null;
        public object data = null;
    }

    interface ILinkedList
    {
        int getLength();
        bool isEmpty();

        void Add(object data);    // append
        void AddTop(object data); // prepend
        void Add(int index, object data); 

        void RemoveTop();
        void RemoveRear();
        void Remove(int index);
        
        void Update(object oldData, object newData);
        void Update(int index, object newData);

        int getIndex(object data);
        object getData(int index);

        object this[int index] { get; set; }
        void print();

    }

    class LinkedList: ILinkedList
    {
        // provide access to derived classes
        protected Node head = null;
        protected Node tail = null;
        protected int listLength = 0;

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

        public void Add(int index, object data)
        {
            // if empty create new lust
            if(isEmpty())
            {
                Add(data);
                return;
            }

            // error checking for index value
            if(index < 0 || index > getLength())
            {
                Console.WriteLine("[ERROR] Add(int,object): {0} is an invalid index", index);
                return;
            }

            // prepend if index 0
            if(index == 0)
            {
                AddTop(data);
                return;
            }

            // append if index = number of items
            if(index == getLength())
            {
                Add(data);
                return;
            }

            Node node = new Node();
            node.data = data;

            int i = 0;
            Node curr = head;

            while (i < index - 1)
            {
                curr = curr.next;
                i++;
            }

            // add after curr;
            node.next = curr.next;
            curr.next = node;

            listLength++;

        }

        public void RemoveTop()
        {
            if(isEmpty())
            {
                Console.WriteLine("[WARNING] RemoveTop(): list is empty");
                return;
            }

            head = head.next;
            listLength--;
        }

        public void RemoveRear()
        {
            if(isEmpty())
            {
                Console.WriteLine("[WARNING] RemoveRear(): list is empty");
                return;
            }
            
            Node curr = head;

            while(curr.next != tail)
            {
                curr = curr.next;
            }

            if(curr == head) head = null; //there was only one item, now list is empty
            else 
            {
                curr.next = null;
                tail = curr;
            }

            listLength--;
        }

        public void Remove(int index)
        {
            if(isEmpty())
            {
                Console.WriteLine("[WARNING] Remove(int): list is empty");
                return;
            }

            if(index <  0 || index >= getLength())
            {
                Console.WriteLine("[ERROR] Remove(int): {0} is an invalid index", index);
                return;
            }

            if(index == 0)
            {
                RemoveTop();
                return;
            }

            if(index == getLength() - 1)
            {
                RemoveRear();
                return;
            }

            int i = 0;
            Node curr = head;
            while( i < index - 1)
            {
                curr = curr.next;
                i++;
            }
            
            //Remove the node after curr node;
            curr.next = curr.next.next;

            listLength--;
        }

        public void Update(object oldData, object newData)
        {
            int index = getIndex(oldData);
            if(index != -1)
            {
                Update(index, newData);
                return;
            }

            Console.WriteLine("[ERROR] Update(object,object): {0} not found", oldData);
        }

        public void Update(int index, object newData)
        {
            if(isEmpty())
            {
                Console.WriteLine("[ERROR] Update(int,object): list is empty");
                return;
            }

            if(index < 0 || index >= getLength())
            {
                Console.WriteLine("[ERROR] getIndex(object): {0} is an invalid index", index);
                return;
            }

            Node curr = head;
            int i = 0;
            while(i != index)
            {
                curr = curr.next;
                i++;
            }

            curr.data = newData;
        }

        public int getIndex(object data)
        {
            if(isEmpty())
            {
                Console.WriteLine("[ERROR] getIndex(object): list is empty");
                return -1;
            }

            int index = 0;
            Node curr = head;

            while(curr != null)
            {
                if(curr.data.Equals(data)) return index;
                index++;
            }

            Console.WriteLine("[INFO] getIndex(object): {0} does not exist", data);
            return -1;

        }

        public object getData(int index)
        {
            if(isEmpty())
            {
                Console.WriteLine("[ERROR] getData(int): list is empty");
                return null;
            }

            if(index < 0 || index >= getLength())
            {
                Console.WriteLine("[ERROR] getData(int): {0} is an invalid index", index);
                return null;
            }

            int i = 0;
            Node curr = head;

            while(i != index)
            {
                curr = curr.next;            
                i++;
            }

            return curr.data;
            
        }

        public object this[int index]
        {
            get { return getData(index); }
            
            set { Update(index,  value);  }
        }

        public int getLength() { return listLength; }
        public bool isEmpty() { return getLength() <= 0 ? true: false; }

        public void print()
        {
            int index = 0;
            Node curr = head;

            while(curr != null)
            {
                Console.WriteLine("[{0}]=>{1}", index, curr.data);
                
                curr = curr.next;
                index++;
            }
            Console.WriteLine("Length = " + getLength());
        }
    }
}