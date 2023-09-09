using System;

namespace MEthodOverRidingDemo
{
    class Parent
    {
        #region Public methods

        public virtual void Print()
        {
            Console.WriteLine("This is a Method of Parent Class.");
        }

        #endregion
    }

    class Child : Parent
    {
        #region Public methods

        public override void Print()
        {
            // base.Print();
            Console.WriteLine("This is a Method of Child Class.");
        }

        #endregion
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
