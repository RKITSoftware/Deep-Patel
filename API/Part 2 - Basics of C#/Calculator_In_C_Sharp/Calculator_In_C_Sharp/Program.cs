using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_In_C_Sharp
{
    internal class Program
    {
        public static void Addition(int num1, int num2)
        {
            int result = num1 + num2;
            Console.WriteLine("Addition result of {0} and {1} is {2}", num1, num2, result);
        }

        public static void Subtraction(int num1, int num2)
        {
            int result = num1 - num2;
            Console.WriteLine("Subtraction result of {0} and {1} is {2}", num1, num2, result);
        }

        public static void Multiplication(int num1, int num2)
        {
            int result = num1 * num2;
            Console.WriteLine("Multiplication result of {0} and {1} is {2}", num1, num2, result);
        }

        public static void Divison(int num1, int num2)
        {
            int result = num1 / num2;
            Console.WriteLine("Divison result of {0} and {1} is {2}", num1, num2, result);
        }

        static void Main(string[] args)
        {
            string choice;

            do
            {
                Console.WriteLine("Enter first number");
                int num1 = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter second number");
                int num2 = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the operations which you want to perform? ( +, -, *, /)");
                char ch = char.Parse(Console.ReadLine());

                switch (ch)
                {
                    case '+':
                        Addition(num1, num2);
                        break;
                    case '-':
                        Subtraction(num1, num2);
                        break;
                    case '*':
                        Multiplication(num1, num2);
                        break;
                    case '/':
                        Divison(num1, num2);
                        break;
                    default:
                        Console.WriteLine("Enter operand which shown in braces.");
                        break;
                }

                Console.WriteLine("Do you want to continue? ( yes / no )");
                choice = Console.ReadLine().ToLower();
                
                Console.WriteLine();
            } while (choice == "yes");
        }
    }
}
