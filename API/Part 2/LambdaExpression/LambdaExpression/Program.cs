using System;

namespace LambdaExpression
{
    public delegate void MyDelegate(int num);

    internal class Program
    {
        static void Main(string[] args)
        {
            MyDelegate obj = (num) =>
            {
                num += 5;
                Console.WriteLine(num);
            };

            obj.Invoke(10);
        }
    }
}
