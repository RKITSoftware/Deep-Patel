using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolumorphismDemo
{
    internal class Program
    {
        #region Public methods
        public static void Add()
        {
            int a = 20;
            int b = 30;
            int c = a + b;
            Console.WriteLine(c);
        }

        public static void Add(int a, int b)
        {
            int c = a + b;
            Console.WriteLine(c);
        }

        public static void Add(string a, string b)
        {
            string c = a + " " + b;
            Console.WriteLine(c);
        }

        #endregion

        static void Main(string[] args)
        {
            Add();
            Add(10, 20);
            Add("Deep", "Patel");
        }
    }
}
