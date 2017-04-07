/* 
07th of April 2017
Basic implementation of Deque (pronounce "Deck") using linked list. 
It is a double ended queue, where
data can be inserted and removed from front and rear.
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

     interface IcDeque
     {         
         void AddFront(object data); // O(1)
         void AddRear(object data);  // O(1)

         object RemoveFront();       // O(1)
         object RemoveRear();        // O(n) to search and O(1) to remove

         object FrontElem();         // O(1)
         object RearElem();          // O(1)

         int  getSize();
         bool isEmpty();

         void print();
     }

     class cDeque: IcDeque
     {
         protected Node head = null;
         protected Node tail = null;
         protected int  size = 0;

         public void AddFront(object data)
         {
            // create new node
            Node node = new Node();
            node.data = data;
            node.next = null;

            if(isEmpty())
            {
                // create deque
                head = node;
                tail = head;
            }
            else 
            {
                // prepend
                node.next = head;
                head = node;
            }

            size++;
         }

         public void AddRear(object data)
         {
             // if empty than create a new deque
             if(isEmpty())
             {
                 AddFront(data);
                 return;
             }

             Node node = new Node();
             node.data = data;
             node.next = null;

             // appending
             tail.next = node;
             tail = node;

             size++;

         }

         public object RemoveFront()
         {
             if(isEmpty())
             {
                 Console.WriteLine("[ERROR] RemoveFront(): deque is empty");
                 return null;
             }

             object o = head.data;
             
             if(getSize() == 1)
             {
                head = null;
                tail = null;
             }
             else
             {             
                head = head.next;
             }
             size--;
             return o;
         }

         public object RemoveRear()
         {
             if(isEmpty())
             {
                 Console.WriteLine("[ERROR] RemoveRear(): deque is empty");
                 return null;
             }

             object o = tail.data;

             Node curr = head;
             while(curr.next != tail)
             {
                 curr = curr.next;
             }

             if(curr == head)
             {
                 head = null;
                 tail = null;
             }
             else
             {
                curr.next = null;
                tail = curr;
             }

             size--;
             return o;
         }

         public object FrontElem()
         {
             if(isEmpty())
             {
                 Console.WriteLine("[ERROR] FrontElem(): deque is empty");
                 return null;
             }

             return head.data;

             //do not decrement size;
         }

         public object RearElem()
         {
            if(isEmpty())
            {
                Console.WriteLine("[ERROR] RearElem(): deque is empty");
                return null;
            }

            return tail.data;

            // do not decrement size;
         }

         public int getSize()  { return size; }
         public bool isEmpty() { return getSize() <= 0 ? true : false; }

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
             Console.WriteLine("queue size = " + getSize());

         }
     }
}