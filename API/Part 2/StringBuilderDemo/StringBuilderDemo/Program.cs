using System;
using System.Diagnostics;
using System.Text;

namespace StringBuilderDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // string s1 = "Deep";

            // Stopwatch sw1 = new Stopwatch();
            // sw1.Start();

            // for (int i = 0; i < 100000; i++)
            // {
            //    s1 += i;
            // }
            // sw1.Stop();

            // StringBuilder sb = new StringBuilder("Deep");
            // Stopwatch sw2 = new Stopwatch();

            // sw2.Start();
            // for(int i = 0; i < 100000; i++)
            // {
            //     sb.Append(i);
            // }
            // sw2.Stop();

            // Console.WriteLine("Time taken by String :- {0}", sw1.ElapsedMilliseconds);
            // Console.WriteLine("Time taken by String Builder :- {0}", sw2.ElapsedMilliseconds);

            StringBuilder sb = new StringBuilder(value: "Deep Patel");
            StringBuilder sb1 = new StringBuilder(value: "Deep Patel", capacity: 50);

            // Capacity
            Console.WriteLine(value: "Capacity :- " + sb.Capacity + " " + sb1.Capacity);

            // Append
            sb.AppendLine(value: " Hello World");
            sb.Append(value: "1 ");

            // C specifies Currency
            // N specifies in Amount
            // X specifies Hexa Decimal Value
            sb.AppendFormat(format: "{0:X}", args: 25);

            // Insert Method
            sb.Insert(index: 5, value: "R. ");

            // Remove 
            sb.Remove(startIndex: 14, length: 6);

            // Replace
            sb.Replace(oldValue: "World", newValue: "C#");

            // ToString method
            string str = sb.ToString();
            Console.WriteLine(str);

            Console.WriteLine(sb);
        }
    }
}
