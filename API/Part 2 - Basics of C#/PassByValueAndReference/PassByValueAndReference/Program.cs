using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassByValueAndReference
{
    internal class Program
    {
        public static void PassByValue(int a)
        {
            a = a + 10;
            Console.WriteLine("The value of a is " + a);
        }

        public static void PassByReference(ref int b)
        {
            b = b + 10;
            Console.WriteLine("The value of a is " + b);
        }

        public static void PassByOut(out int value)
        {
            value = 20;
            Console.WriteLine("The value of a is " + value);
        }

        static void Main(string[] args)
        {
            int a = 10;
            PassByValue(a);
            Console.WriteLine("Pass by Value :- " + a);

            int b = 1;
            PassByReference(ref b);
            Console.WriteLine("Pass by Reference :- " + b);

            int value;
            PassByOut(out value);
            Console.WriteLine("Pass by Out :- " + value);

            // Pass by Reference
            // -> The ref keyword causes arguments to be passed in a method by reference.
            // -> In call by reference, the called method changes the value of parameters passed to it from the calling method

            // Pass by Out
            // -> The out keyword is similar to the ref keyword and causes arguments to be passed by reference.
            // -> The only difference between the two is that the out keyword does not require the variables that are
            //    passed by reference to be initialized.

            // Both the called method aand the calling method must explicitly use the ref and out keyword.
        }
    }
}
