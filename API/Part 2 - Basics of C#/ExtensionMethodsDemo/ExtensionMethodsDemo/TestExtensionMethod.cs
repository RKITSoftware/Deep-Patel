using System;

namespace ExtensionMethodsDemo
{
    internal class TestExtensionMethod
    {
        static void Main()
        {
            Program prog = new Program();
            prog.Func3(1);
            
            int i = 20;
            bool result = i.IsGreaterThan(10);
            Console.WriteLine(result);
        }
    }
}
