using System;

namespace StructureDemo
{
    internal struct Program
    {
        int a;

        public void Func1()
        {
            Console.WriteLine("This is my new function.. " + a);
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Func1();
        }
    }
}
