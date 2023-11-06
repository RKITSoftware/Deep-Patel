using System;

namespace SealedClass
{
    /* // Sealed Class Example
    sealed class BaseClass
    {
        public void Show1()
        {
            Console.WriteLine("Method of base class.");
        }
    }

    class DerivedClass : BaseClass
    {
        public void Show2()
        {
            Console.WriteLine("Method of derived class.");
        }
    }

    */

    // Sealed method example
    class A
    {
        public virtual void Print()
        {
            Console.WriteLine("This is a method of class A");
        }
    }

    class B : A
    {
        public sealed override void Print()
        {
            Console.WriteLine("This is a method of class B");
        }
    }

    class C : B
    {
        public override void Print()
        {
            Console.WriteLine("This is a method of class C");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Sealed Class Example
            // DerivedClass dc = new DerivedClass();
            // dc.Show1();
            // dc.Show2();

            C obj = new C();
            obj.Print();
        }
    }
}
