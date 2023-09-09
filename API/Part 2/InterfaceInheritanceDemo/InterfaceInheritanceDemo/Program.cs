using System;

namespace InterfaceInheritanceDemo
{
    // Interface Inheritance Chain

    interface I1
    {
        #region Public Methods
        void Print1();
        
        #endregion
    }

    interface I2
    {
        #region Public Methods
        void Print2();

        #endregion
    }

    interface I3 : I1, I2
    {
        #region Public Methods
        void Print3();

        #endregion
    }

    internal class Program : I3
    {
        #region Public Methods
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

        #endregion

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Print1();
            p.Print2();
            p.Print3();
        }
    }
}
