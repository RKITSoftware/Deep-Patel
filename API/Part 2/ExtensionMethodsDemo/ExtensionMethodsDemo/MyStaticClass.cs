using System;

namespace ExtensionMethodsDemo
{
    static class MyStaticClass
    {
        public static void Func3(this Program p, int i)
        {
            Console.WriteLine("This is third function");
            Console.WriteLine(i);
        }

        public static bool IsGreaterThan(this int i, int value)
        {
            return i > value;
        }
    }
}
