using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarAndDynamicKeyword
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             * - var was introcued in C# 3.0
             * - var keyword is used to store any type of data in its variable
             * - Value of var is decided at compile time
             * - We have to initialize the variable with var keyword
             * - If we want to check the type of value which is stored in var variable
             *   then we can use GetType() method with the var variable.
             * - You can not change the data type after initializing var.
             * - We can not use var variable as a function parameter.
             * - var keyword is a value type.
            */

            var name = "Deep";
            var age = 21;
            Console.WriteLine(age);
            Console.WriteLine(name.GetType());

            /*
             * - dynamic was introduced in C# 4.0
             * - dynamic keyword is also used to store any type of data in its variable.
             * - value of dynamic variable is defined at run time.
             * - Initialization is not mandatory when we declare a variable with dynamic keyword.
             * - GetType() returns type of value stored in dynamic keyword.
             * - we can change the value of dynamic keyword
             * - Dynamic keyword does not support intellisense.
             * - dynamic variables can be used to create properties and return values from a function
             * - we can use dynamic variable as a function parameter
             * - dynamic keyword is of reference type
            */

            dynamic user = "Deep Patel";
            dynamic a;
            a = 25;
            Console.WriteLine(a.GetType());
            a = 'D';
            Console.WriteLine(a.GetType());
        }
    }
}
