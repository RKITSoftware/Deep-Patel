using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enumerationdemo
{
    // User Defined Enum
    enum Days
    {
        Sunday = 1, // 0
        Monday, // 1
        Tuesday, // 2
        Wednesday, // 3
        Thursday, // 4
        Friday, // 5
        Saturday // 6
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // System Class Enums
            // Console.BackgroundColor = ConsoleColor.Yellow;
            // Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(Days.Sunday);

            // Days birthDay = Days.Monday;
            // Days day = (Days)5;
            // int val = (int)Days.Saturday;
            // Console.WriteLine(birthDay);
            // Console.WriteLine(day);
            // Console.WriteLine(val);

            Console.WriteLine("Enter your name");
            string name = Console.ReadLine();

            Console.WriteLine("Enter your birthday day? sunday = 1, monday = 2, ...");
            int value = int.Parse(Console.ReadLine());

            Days myDay = (Days)value;
            Console.WriteLine("My name is {0} and my birthday day is {1}\n", name, myDay);

            string[] members = (string[])Enum.GetNames(typeof(Days));
            foreach(string member in members)
            {
                Console.WriteLine(member);
            }
            Console.WriteLine();

            int[] values = (int[])Enum.GetValues(typeof(Days));
            foreach (int v in values)
            {
                Console.WriteLine(v);
            }
        }
    }
}
