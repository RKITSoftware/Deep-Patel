using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            // Assignment Operator
            // - Simple => =
            // - Compound => +=, -=, *=, /=, %=

            // Increment && Decrement Operators
            // a++, ++a, --a, a--

            // Ternary Operator
            // Boolean Expression ? First Statement : Second Statement
            int e = a > b ? 10 : 20;
            Console.WriteLine(e);

            // Precedence of Operator
            // Highest Level -> (), /, *, +, -
        }
    }
}
