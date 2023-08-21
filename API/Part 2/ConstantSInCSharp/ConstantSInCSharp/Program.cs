using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstantSInCSharp
{
    internal class Program
    {
        public const double PI = 3.14d;
        public const string companyName = "RKIT";

        static void Main(string[] args)
        {
            // A constant has a fixed value that remains unchanged throughout the program
            // You have to initialize constant at the time of it's declaration
            // Constant are value typed not reference type

            Console.WriteLine(PI);
            Console.WriteLine(companyName);
            Console.ReadLine();
        }
    }
}
