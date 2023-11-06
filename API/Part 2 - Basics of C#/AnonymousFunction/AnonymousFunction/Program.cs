using System;

namespace AnonymousFunction
{
    public delegate void MyDelegate(int num);

    internal class Program
    {
        // public static void MyMethod(int a) 
        // {
        //     a += 10;
        //     Console.WriteLine(a);
        // }

        static void Main(string[] args)
        {
            // MyDelegate obj = new MyDelegate(MyMethod);
            // obj.Invoke(10);

            // Anonymous Function
            MyDelegate obj = delegate (int num)
            {
                num += 10;
                Console.WriteLine(num);
            };

            obj.Invoke(10);
        }
    }
}