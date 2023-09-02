using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodHidingDemo
{
    class Parent
    {
        public void Show()
        {
            Console.WriteLine("Parent Class Method");
        }
    }

    class Child : Parent
    {
        public new void Show()
        {
            // base.Show();
            Console.WriteLine("Child Class Method");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Parent p = new Child();
            p.Show();

            Child c = new Child();
            // c.Show();
            ((Parent)c).Show();
            c.Show();
        }
    }
}
