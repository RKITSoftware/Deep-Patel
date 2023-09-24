using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringClassDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string str1 = "This is a string created by assignment";
            Console.WriteLine(str1);

            string str2 = "The path is C:\\Users\\DEEP";
            Console.WriteLine(str2);

            // verbatim literal
            string str3 = @"The path is C:\Users\DEEP";
            Console.WriteLine(str3);

            // Create a string from character array.
            char[] chars = { 'w', 'o', 'r', 'd' };
            string str4 = new string(chars);
            Console.WriteLine(str4);

            // Create a string that consists of a character repeated 10 times.
            string str5 = new string('d', 10);
            Console.WriteLine(str5);

            // Concatenation
            string str6 = "Today is " + DateTime.Now.ToString("D") + ".";
            Console.WriteLine(str6);

            // Extract second word
            string str7 = "This sentence has five words.";
            int startPosition = str7.IndexOf(" ") + 1;
            string word2 = str7.Substring(startPosition, str7.IndexOf(" ", startPosition) - startPosition);
            Console.WriteLine(word2);

            // Format method
            DateTime dateAndTime = new DateTime(2011, 7, 6, 7, 32, 0);
            double temperature = 68.3;
            string result = String.Format("At {0:t} on {0:D}, the temperature was {1:F1} degrees Fahrenheit.",
                              dateAndTime, temperature);
            Console.WriteLine(result);

            // ToUpper Method
            string str8 = str1.ToUpper();
            Console.WriteLine(str8);

            // ToLower Method
            string str9 = str8.ToLower();
            Console.WriteLine(str9);

            // Replace Method
            string str10 = str9.Replace('a', 'd');
            Console.WriteLine(str10);


        }
    }
}
