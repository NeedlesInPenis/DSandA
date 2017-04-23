using System;

namespace adt
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Testing different data structures");
            Avltree b = new Avltree();
            b.insert(1, "");
            b.insert(2, "");
            b.insert(3, "");
            b.insert(4, "");
            b.insert(5, "");
            b.insert(6, "");
            b.insert(7, "");
        }
    }
}

