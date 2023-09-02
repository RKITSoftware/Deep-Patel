using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceInheritanceDemo
{
    // Interface Inheritance Chain

    interface I1
    {
        void Print1();
    }

    interface I2
    {
        void Print2();
    }

    interface I3 : I1, I2
    {
        void Print3();
    }

    internal class Program : I3
    {
        public void Print1()
        {
            Console.WriteLine("Method of Interface 1");
        }

        public void Print2()
        {
            Console.WriteLine("Method of Interface 2");
        }

        public void Print3()
        {
            Console.WriteLine("Method of Interface 3");
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Print1();
            p.Print2();
            p.Print3();
        }
    }
}
