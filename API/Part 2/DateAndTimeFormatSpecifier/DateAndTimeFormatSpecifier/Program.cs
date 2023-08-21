using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateAndTimeFormatSpecifier
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // A date and time format specifier is a special character that
            // enables you to display the date and time values in different formats.

            /*
             * Format Specifier     Name
             * d                    Short Date
             * D                    Long Date
             * f                    full date / time (short time)
             * F                    Full date / time (long time)
             * g                    General date / time (short time)
             * G                    General date / time (long time)
             * m or M               Month Day
             * t                    short time
             * T                    Long time
             * y or Y               Year Month Pattern
             * ddd                  Represents the abbreviated name of the week
             * dddd                 Represents the full name of the week
             * FF                   Represents the two digits of the seconds fraction
             * HH                   Represents the hour from 00 to 23
             * MM                   Represents the month from 01 to 12
             * MMM                  Represents the abbreviated name of the month
             * ss                   Represents the seconds from 00 to 59
            */

            DateTime dt = DateTime.Now;
            Console.WriteLine("{0}", dt);
            Console.WriteLine("{0:d}", dt);
            Console.WriteLine("{0:D}", dt);
            Console.WriteLine("{0:f}", dt);
            Console.WriteLine("{0:F}", dt);
            Console.WriteLine("{0:g}", dt);
            Console.WriteLine("{0:G}", dt);
            Console.WriteLine("{0:m}", dt);
            Console.WriteLine("{0:t}", dt);
            Console.WriteLine("{0:T}", dt);
            Console.WriteLine("{0:y}", dt);
            Console.WriteLine("{0:HH:mm:ss tt}", dt);
            Console.WriteLine("{0:dd-MM-yyyy}", dt);

            Console.ReadLine();
        }
    }
}
