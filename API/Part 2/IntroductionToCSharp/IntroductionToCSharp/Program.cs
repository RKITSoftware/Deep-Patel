using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroductionToCSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            Console.WriteLine("Enter your first name");
            string fname = Console.ReadLine();

            Console.WriteLine("Enter your last name");
            string lname = Console.ReadLine();

            Console.WriteLine("Enter your name is " + fname); // Concatenation
            Console.WriteLine("Your name is {1} {0}", fname, lname); // Placeholder Syntax
            */

            /* Takes two number from user
            Console.WriteLine("Enter first number");
            int num1 = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter second number");
            int num2 = int.Parse(Console.ReadLine());

            // Addition of two number
            int sum = num1 + num2;
            Console.WriteLine("Addition Result is {0}.", sum);
            */

            // Integer Data types

            /*
            // sbyte range is -128 to 127
            sbyte num = -34;

            // byte range is 0 to 255
            byte age = 255;

            // char range is U + 0000 to U + ffff
            char ch = 'D';

            // short range is -32768 to 32767
            short ans = 3456;

            // ushort range is 0 to 65535
            ushort ans1 = 45679;

            // int range is -2147483648 to 2147483647
            int range = 426216865;

            // uint range is 0 to 4294967295
            uint range1 = 4126835653;

            // long range is -9223372036854775808 to 9223372036854775807
            long longVal = 523686215652655987;

            // ulong range is 0 to 18446744073709551615
            ulong longVal2 = 5236862156526559871;

            // MinValue and MaxValue gives range
            Console.WriteLine(int.MinValue + " " + int.MaxValue);

            bool has two values true or false.
            bool isValid = true;
            Console.WriteLine(isValid);
            */

            // Float Data Types

            /*
            // Float size is 4 bytes and precision is 7 digits
            // Float prefix is f
            float number = 77334242424.24321112f;
            Console.WriteLine(number);

            // Double size is 8 bytes and precision is 15-16 digits
            // Double prefix is d
            double number2 = 1232373456.14432663213456;
            Console.WriteLine(number2);

            // Decimal size is 16 bytes and precision is 28-29 digits
            // Decimal prefix is m
            decimal number3 = 247364713405735.473785734237846783462754752738m;
            Console.WriteLine(number3);
            */

            /*
            
            // String and Character Data Type
            // Escape Sequence
            // \"   \'  \t  \\  \n

            string a = "'Welcome to c#'";
            char ch = 'j';

            Console.WriteLine(a);

            // Verbatim Literal
            // String with @ Symbol.
            // It make escape sequence translate as normal printable characters to enhance readability.

            string x = @"C:\Deep\Tutorials\CSharp";
            Console.WriteLine(x);
            */
            Console.ReadLine();
        }
    }
}
