using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestartCSharpProgWithUserInput
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string confirm;

            do
            {
                Console.WriteLine("Enter number 1");
                int number1 = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter number 2");
                int number2 = int.Parse(Console.ReadLine());

                int add = number1 + number2;
                Console.WriteLine("Addition result is " + add + "\n");

                Console.WriteLine("Do you want to repeat your program? Yes / No");
                confirm = Console.ReadLine().ToLower();
            } while (confirm == "yes");
        }
    }
}
