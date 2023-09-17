using System;

namespace ExceptionDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             
            // DividedByZeroException
            
            Console.WriteLine("Enter first number");
            int num1 = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter second number");
            int num2 = int.Parse(Console.ReadLine());

            try
            {
                int result = num1 / num2;
                Console.WriteLine($"Division result is {result}.");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("You cannot divide a number by zero...");
            }

            */

            /*
            
            // IndexOutOfRangeException

            int[] myArr = { 1, 2, 3 };

            try
            {
                myArr[3] = 10;
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Index was outside the bounds of the array");
            }

            foreach (int val in myArr)
            {
                Console.WriteLine(val);
            }

            */

            /*
            
            // Null reference exception

            try
            {
                string name = null;
                Console.WriteLine(name.Length);
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("String is null..");
            }

            */

            /*
            
            // FormatException

            Console.WriteLine(value: "Enter a number");
            string number = Console.ReadLine();

            try
            {
                int num = int.Parse(s: number);
                Console.WriteLine(value: $"Number is :- {num}");
            }
            catch (FormatException)
            {
                Console.WriteLine("String format is invalid..");
            }

            */

            /*
            try
            {
                // int a = 10;
                // int b = 0;
                // int c = a / b;

                // string a = null;
                // Console.WriteLine(a.Length);

                int[] arr = { 1, 2, 3, 2};
                arr[4] = 12123;
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Finally block executed...");
            }
            */

            // Default Throw and Default Catch
            // int a = 10;
            // int b = 0;
            // int c = a / b;
            // Console.WriteLine(c);

            // Default Throw and Our Catch
            // try
            // {
            //     string name = null;
            //     Console.WriteLine(name.Length);
            // }
            // catch (NullReferenceException)
            // {
            //     Console.WriteLine("String is null...");
            // }

            // Our Throw and Default Catch
            // Console.Write("Enter your age: ");
            // int age = int.Parse(Console.ReadLine());

            // if (age >= 18)
            // {
            //     Console.WriteLine("You are eligible to vote.");
            // }
            // else
            // {
            //     throw new Exception("You are not eligible to vote.");
            // }

            // Our Throw and Our Catch
            // Console.Write("Enter your age: ");
            // int age = int.Parse(Console.ReadLine());

            // try
            // {
            //     if (age >= 18)
            //     {
            //         Console.WriteLine("You are eligible to vote.");
            //     }
            //     else
            //     {
            //         throw new Exception("You are not eligible to vote.");
            //     }
            // }
            // catch(Exception ex)
            // {
            //     Console.WriteLine(ex.Message);
            // }

            int accountBalance = 5000;
            int withdrawAmount = 6000;

            try
            {
                if (accountBalance < withdrawAmount)
                {
                    throw new Exception(message: "Insufficient Amount.");
                }
                else
                {
                    accountBalance -= withdrawAmount;
                    Console.WriteLine($"Remaining balance is :- {accountBalance}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}