using System;

namespace ExtensionMethodsDemo
{
    internal class Program
    {
        public void Func1()
        {
            Console.WriteLine("This is first function");
        }

        public void Func2()
        {
            Console.WriteLine("This is second function");
        }

        static void Main(string[] args)
        {
            Program prog = new Program();
            prog.Func1();
            prog.Func2();
        }
    }
}
