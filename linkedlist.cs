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
        /* NOTES:
            [O(n - 1) + O(1)] means:
            it take O(n - 1) to search the data and O(1) to perform the operation.
            In many case O(n-1) is eliminated and data processing is done in constant time.
         */

        int getLength();
        bool isEmpty(); 

        void Add(object data);    // append. O(1)
        void AddTop(object data); // prepend. O(1) 
        void Add(int index, object data); // insert add arbitary position. [O(n - 1) + O(1)]

        void RemoveTop();  // O(1)
        void RemoveRear(); // 0(n-1) + O(1)
        void Remove(int index); // remove from arbitary position. 0(n-1) + O(1)
        
        void Update(object oldData, object newData); // replace data. O(n)
        void Update(int index, object newData);      // replace data at index. O(n)

        int getIndex(object data); // get index of a data, -1 if not found. O(n)
        object getData(int index); // get data at index, null if bad index. O(n) [O(1) for head and tail]

        object this[int index] { get; set; } // overloading [] operator 
        void print(); // dumping the list

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
            // if empty create new list
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

            while (i < index - 1) // index - 1 is important
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

            if(index == getLength() - 1) // getLength() - 1 is impotant
            {
                RemoveRear();
                return;
            }

            int i = 0;
            Node curr = head;
            while( i < index - 1) // index - 1 is important
            {
                curr = curr.next;
                i++;
            }
            
            //Remove the node after curr node;
            curr.next = curr.next.next;

            listLength--;
        }

        // replacing data
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

        // replacing data with index
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

            if(index == 0) return head.data;

            if(index == getLength() - 1) return tail.data;

            int i = 0;
            Node curr = head;

            while(i != index)
            {
                curr = curr.next;            
                i++;
            }

            return curr.data;
            
        }

        // this look cool
        public object this[int index]
        {
            get { return getData(index); }
            
            set { Update(index,  value);  }
        }

        public int getLength() { return listLength; }
        public bool isEmpty() { return getLength() <= 0 ? true: false; }

        // dumping whole list
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