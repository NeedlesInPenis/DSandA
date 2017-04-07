/*
    07th of April 2017
    Basic implementation of Queue using linked list
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

     interface IcQueue
     {
        bool isEmpty();
        int getSize();

        void enqueue(object data); // O(1)
        object dequeue();          // O(1)

        void print();
     }

     class cQueue: IcQueue
     {
         protected Node head = null;
         protected Node tail = null;
         protected int  size = 0;

         public int  getSize() { return size; }
         public bool isEmpty() { return getSize() <= 0 ? true: false; }

         public void enqueue(object data)
         {
             //creating node
             Node node = new Node();
             node.data = data;
             node.next = null;

             if(isEmpty())
             {
                 // creating queue
                 head = node;
                 tail = head; // updating tail pointer
             }
             {
                 // appending
                 tail.next = node;
                 tail = tail.next; // updating tail pointer
             }

             size++;
         }

         public object dequeue()
         {
             if(isEmpty())
             {
                 Console.WriteLine("[ERROR] dequeue(): Queue is empty");
                 return null;
             }

             object o = head.data;
             head = head.next;
             
             size--;
             return o;
         }

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