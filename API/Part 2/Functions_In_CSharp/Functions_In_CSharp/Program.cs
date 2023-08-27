using System;

namespace Functions_In_CSharp
{
    internal class Program
    {
        // Syntax for function
        // <Access Specifier> <Return Type> <Method Name> (Parameter list)
        // {
        //      Method Body
        // }

        // non-static / instance method
        public void PrintHelloWorld() // Declare a method
        {
            Console.WriteLine("Hello World");
        }

        public static void PrintHelloWorld2() // Declare a method
        {
            Console.WriteLine("Hello World from static method");
        }

        // Default Value
        public static void PrintUserName(string userName = "Unknown")
        {
            Console.WriteLine("Hello {0}", userName);
        }

        public static int Addition(int numberOne, int numberTwo)
        {
            return numberOne + numberTwo;
        }

        public static void ShowNameAge(string name, int age)
        {
            Console.WriteLine("Your name is " + name);
            Console.WriteLine("Your age is " + age);
        }

        static void Main(string[] args)
        {
            // A Methods is a grpup of statements that together performs a task
            // It is used to perform specific task.
            // Methods are reusable.

            // - Built in methods
            // - User defined methods

            Program p1 = new Program();
            p1.PrintHelloWorld(); // Call

            PrintHelloWorld2();
            PrintUserName("Deep");
            PrintUserName("Janvi");
            PrintUserName();

            Console.WriteLine("Addition of 5 and 10 is :- {0}", Addition(5, 10));

            ShowNameAge(age : 21, name : "deep"); // named aruguments

            // Position Wise, Default Arguments, Named aruguments

            // When calling a method at that time passes values are called as arguments
            // and when declaring a method and it takes arguments are called as parameters.
        }
    }
}
