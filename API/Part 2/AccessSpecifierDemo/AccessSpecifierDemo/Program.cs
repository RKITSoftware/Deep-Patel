using System;

namespace AccessSpecifierDemo
{
    public class class1
    {
        protected void show1 ()
        {
            // Console.WriteLine("This is a public method");
            Console.WriteLine("This is a protected method");
        }

        public void show2()
        {
            class1 c1 = new class1();
            c1.show1();
        }
    }

    internal class Program : class1
    {
        static void Main(string[] args)
        {
            // - public
            // - private - By Default
            // - protected
            // - internal

            // class1 c1 = new class1();
            // c1.show1();
            // c1.show2();

            Program p1 = new Program();
            p1.show1();
        }
    }
}
