using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEthodOverRidingDemo
{
    class Parent
    {
        public virtual void Print()
        {
            Console.WriteLine("This is a Method of Parent Class.");
        }
    }

    class Child : Parent
    {
        public override void Print()
        {
            base.Print();
            // Console.WriteLine("This is a Method of Child Class.");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Parent p = new Child();
            Child c = new Child();
            p.Print();
            c.Print();
        }
    }
}
