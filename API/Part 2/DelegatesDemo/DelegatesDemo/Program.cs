using System;

namespace DelegatesDemo
{
    /// <summary>
    /// References other methods
    /// </summary>
    /// <param name="num1">First number</param>
    /// <param name="num2">Second number</param>
    public delegate void Calculation(int num1, int num2);

    internal class Program
    {
        public static void Addition(int a, int b)
        {
            int result = a + b;
            // Console.WriteLine(format: "Addition result is {0}", result);
            Console.WriteLine(value: $"Addition result is {result}");
        }

        public static void Subtraction(int a, int b)
        {
            int result = a - b;
            Console.WriteLine(value: $"Subtraction result is {result}");
        }

        public static void Multiplication(int a, int b)
        {
            int result = a * b;
            Console.WriteLine(value: $"Multiplication result is {result}.");
        }
        static void Main(string[] args)
        {
            Calculation obj = new Calculation(Addition);
            obj.Invoke(40, 20); // 60

            obj += Subtraction;
            obj += Multiplication;
            obj(20, 10); // 30 10 200

            obj -= Subtraction;
            obj(20, 10); // 30 200
        }
    }
}
