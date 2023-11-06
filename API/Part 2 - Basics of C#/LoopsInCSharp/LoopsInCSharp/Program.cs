using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopsInCSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Loops allows you to execute a single statement or a block of statements repetitively.

            int sum = 0;
            for(int i = 1; i <= 10; i++) // Counter variable
            {
                // Console.WriteLine(i);
                sum += i;
            }

            Console.WriteLine("The sum is {0}\n", sum);

            Console.WriteLine("Enter number");
            int number = int.Parse(Console.ReadLine());

            for(int i = 1; i <= 10; i++)
            {
                Console.WriteLine("{0} x {1} = {2}", number, i, number * i);
            }

            Console.WriteLine("\n");

            int num = number;
            int fact = 1;

            while(num >= 1)
            {
                fact *= num;
                num--;
            }

            Console.WriteLine("Tha factorial of {0} is {1} \n", number, fact);

            int a = 1;
            int b = 1;
            Console.Write("{0} {1} ", a, b);

            do
            {
                int c = a + b;
                Console.Write("{0} ", c);
                a = b;
                b = c;
                number--;
            } while (number - 2 > 0);

            Console.WriteLine();

            // Diffrence between for and while loop
            // - In for loop we can't use counter variable outside the loop
            // - But we can use counter variable in while loop

            // Nested loop

            for(int outer = 1; outer <= 5; outer++)
            {
                for(int inner = 1; inner <= outer; inner++)
                {
                    Console.Write("{0} ", inner);
                }
                Console.WriteLine();
            }
        }
    }
}
