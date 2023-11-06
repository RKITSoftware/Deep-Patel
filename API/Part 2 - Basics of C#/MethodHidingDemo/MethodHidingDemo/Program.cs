using System;

namespace MethodHidingDemo
{
    class Parent
    {
        #region Public methods
        public void Show()
        {
            Console.WriteLine("Parent Class Method");
        }

        #endregion
    }

    class Child : Parent
    {
        #region Public methods
        public new void Show()
        {
            // base.Show();
            Console.WriteLine("Child Class Method");
        }

        #endregion
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
