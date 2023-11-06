using System;

namespace Operator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Arithmetic Operator
            // +, -, *, /, %

            int a = 10, b = 5;
            Console.WriteLine("{0} {1} {2} {3} {4}", a + b, a - b, a * b, a / b, a % b);

            // Relational or Comparison Operator
            // ==, !=, <, >, <=, >=

            int c = 10, d = 20;
            Console.WriteLine("{0} {1} {2} {3} {4} {5}", c == d, c != d, c > d, c < d, c >= d, c <= d);

            // Logical or Conditional Operator
            // And (&&)
            // Or (||)
            Console.WriteLine(true && false);
            Console.WriteLine(true || false);

            // Assignment Operator
            // - Simple => =
            // - Compound => +=, -=, *=, /=, %=
            int e = 10;
            e += 2;
            Console.WriteLine(e);

            // Increment && Decrement Operators
            // a++, ++a, --a, a--
            e++;
            Console.WriteLine(e);

            // Ternary Operator
            // Boolean Expression ? First Statement : Second Statement
            int f = a > b ? 10 : 20;
            Console.WriteLine(f);

            // Precedence of Operator
            // Highest Level -> (), /, *, +, -
        }
    }
}
