using System;

namespace adt
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Testing different data structures");
            BinarySearchTree b = new BinarySearchTree();
            b.insert(5);
            b.insert(3);
            b.insert(20);
            b.insert(2);
            b.insert(4);
            b.insert(10);
            b.insert(22);
            b.insert(1);
            b.insert(9);
            b.insert(11);
            b.insert(21);
            b.insert(23);
            b.insert(12);
        }
        /**
        1 2 3 4 5 6 7 8 9 10

           5
    3            20
 2     4    10        22
1          9  11     21  23
                12

         */
    }
}

