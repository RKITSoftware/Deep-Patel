using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpStatements
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Jump statements are used to transfer control form one point in a program to another point.
            // - Break
            // - Continue
            // - goto
            // - return 

            // Break statement
            Console.WriteLine("Break");

            for(int i = 0; i < 10; i++)
            {
                if(i == 5)
                {
                    break;
                }
                Console.WriteLine(i);
            }

            Console.WriteLine("\nContinue");

            for (int i = 0; i < 10; i++)
            {
                if (i == 5)
                {
                    continue;
                }
                Console.WriteLine(i);
            }

            Console.WriteLine("\nGoto");

            for (int i = 0; i < 10; i++)
            {
                if (i == 5)
                {
                    goto stop;
                }
                Console.WriteLine(i);
            }

            stop:
                Console.WriteLine("Program Exits...");
        }
    }
}
