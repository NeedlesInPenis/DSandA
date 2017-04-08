/*
08th of April 2017
Basic implementation of doubly linked list.
*/

using System;
namespace adt
{

    /*
     // "ERROR: namespace `adt` already contains definition for Node."
     class Node 
     {
         public Node   next = null;
         public object data = null;
     }
    */
  

     interface IDoubleLL
     {
         /*
         Notes:
         [O(n) + O(1)]: O(n) to search data and O(1) to process data;
          */
         int  getSize();
         bool isEmpty();

         void Add(object data);    // append data. O(1)
         void AddTop(object data); // prepend data. O(1)
         void Add(int index, object data); // insert at arbitary index. ([o(n-1)+o(1)])

         void RemoveTop(); // O(1)
         void RemoveRear();// O(1)
         void Remove(int index); // remove from arbitary index. ([O(n-1) + O(1)])

         void Update(int index, object newData);       // ([O(n) + O(1)])
         void Update(object oldData, object newData);  // ([O(n) + O(1)])

         int getIndex(object data);                    // O(n)
         object getData(int index);                    // ([O(n) + O(1)])

         object this[int index] { get; set; }
         void print();        // O(n)
         void reversePrint(); // O(n)
     }

     class DoubleLL : IDoubleLL
     {
         protected Node head = null;
         protected Node tail = null;
         protected int   size = 0;

         public void Add(object data)
         {
            // Create new node object
            Node node = new Node();
            node.prev = null;
            node.next = null;
            node.data = data;

            if(isEmpty())
            {
                head = node;
                tail = node;
            }
            else
            {
                // append node;
                node.prev = tail;
                tail.next = node;
                tail = node;      // update tail
            }

            size++;
         }

         public void AddTop(object data)
         {
            if(isEmpty())
            {
                Add(data);
                return;
            }

            Node node = new Node();
            node.data = data;

            node.next = head;
            head.prev = node;
            head = node;

            size++;
         }

         public void Add(int index, object data)
         {
            if(isEmpty())
            {
                Add(data);
                return;
            }

            // note: index can be equal to getSize()
            if(index < 0 || index > getSize())
            {
                Console.WriteLine("[ERROR] Add(int,object): {0} is an invalid index", index);
                return;
            }

            if(index == 0)
            {
                AddTop(data);
                return;
            }

            if(index == getSize())
            {
                Add(data);
                return;
            }

            Node node = new Node();
            node.data = data;

            int i = 0;
            Node curr = head;

            while( i < index - 1) // index - 1 is important
            {
                curr = curr.next;
                i++;
            }

            //insert after curr node;
            node.next = curr.next;
            node.prev = curr;

            curr.next.prev = node;
            curr.next = node;

            size++;
         }
         public void RemoveTop()
         {
             if(isEmpty())
             {
                 Console.WriteLine("[ERROR] RemoveTop(): list is empty");
                 return;
             }

             if(getSize() == 1)
             {
                 head = null;
                 tail = null;
             }
             else
             {
                 head = head.next;
                 head.prev = null;
             }

             size--;
         }

         public void RemoveRear()
         {
             if(isEmpty())
             {
                 Console.WriteLine("[ERROR] RemoveRear(): list is empty");
                 return;
             }

             if(getSize() == 1)
             {
                 head = null;
                 tail = null;
             }
             else
             {
                 tail = tail.prev;
                 tail.next = null;
             }

             size--;
         }

         public void Remove(int index)
         {
             if(isEmpty())
             {
                 Console.WriteLine("[ERROR] Remove(int): list is empty");
                 return;
             }

             // index should be less than size;
             if(index < 0 || index >= getSize())
             {
                 Console.WriteLine("[ERROR] Remove(int): {0} is an invalid index", index);
                 return;
             }

             if(index == 0)
             {
                 RemoveTop();
                 return;
             }

             if(index == getSize() - 1) // getSize() - 1 is important
             {
                 RemoveRear();
                 return;
             }

             int i = 0;
             Node curr = head;

             while(i < index - 1) // index - 1 is important
             {
                 curr = curr.next;
                 i++;
             }

             // remove node after curr;
             curr.next = curr.next.next;
             curr.next.prev = curr;

             /*
             simplified version
             Node nodeToRemove = curr.next;
             curr.next = nodeToRemove.next;
             nodeToRemove.next.prev = nodeToRemove.prev;
              */

              size--;

         }

         public void Update(object oldData, object newData)
         {
             int index = getIndex(oldData);

             if(index == -1)
             {
                 Console.WriteLine("[ERROR] Update(object,object): data not found");
                 return;
             }

             Update(index, newData);
         }
         public void Update(int index, object newData)
         {
             if(isEmpty())
             {
                 Console.WriteLine("[ERROR] Update(int,object): list is empty");
                 return;
             }

             if(index < 0 || index >= getSize())
             {
                 Console.WriteLine("[ERROR] Update(int,object): {0} is an invalid index", index);
                 return;
             }

             if(index == 0)
             {
                 head.data = newData;
                 return;
             }

             if(index == getSize() - 1)
             {
                 tail.data = newData;
                 return;
             }

             int i = 0;
             Node curr = head;

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
                 Console.WriteLine("[ERROR] getIndex(int): list is empty");
                 return -1;
             }

             int index = 0;
             Node curr = head;

             while(curr != null)
             {
                 if(data.Equals(curr.data)) return index;

                 curr = curr.next;
                 index++;
             }

             return -1; // data not found;
         }

         public object getData(int index)
         {
             if(isEmpty())
             {
                 Console.WriteLine("[ERROR] getData(int): list is empty");
                 return null;
             }

             if(index < 0 && index >= getSize())
             {
                 Console.WriteLine("[ERROR] getData(int): {0} is an invalid index", index);
                 return null;
             }

             if(index == 0) return head.data;
             if(index == getSize() - 1) return tail.data;

             int i = 0;
             Node curr = head;

             while( i != index)
             {
                 curr = curr.next;
                 i++;
             }

             return curr.data;
         }

         public object this[int index]
         {
             get { return getData(index); }
             set { Update (index, value); }
         }

         public int  getSize() { return size; }
         public bool isEmpty() { return getSize() <=0 ? true : false; }

         public void print()
         {
             Node curr = head;
             int index = 0;
          
             while(curr != null)
             {
                Console.WriteLine("[{0}]=>{1}", index, curr.data);
                index++;
                curr = curr.next;
             }

             Console.WriteLine("List size = " + getSize());
         }


         public void reversePrint()
         {
             Node curr = tail;
             int index = getSize() - 1;
             while(curr != null)
             {
                 Console.WriteLine("[{0}]=>{1}", index, curr.data);
                 curr = curr.prev;
                 index--;
             }
             Console.WriteLine("List size = " + getSize());
         }
     }
}