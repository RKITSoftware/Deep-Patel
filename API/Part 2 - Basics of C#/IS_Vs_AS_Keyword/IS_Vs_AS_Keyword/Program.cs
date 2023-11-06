using System;

namespace IS_Vs_AS_Keyword
{
    internal class Program
    {
        static void Main(string[] args)
        {
            object a = 'a';
            // Console.WriteLine("A is string :- " + (a is string));

            if(a is string)
            {
                Console.WriteLine("Yes its a string.");
            }
            else
            {
                Console.WriteLine("No its not a string.");
            }

            object b = 456;
            string str = a as string;
            Console.WriteLine(str);
        }
    }
}
