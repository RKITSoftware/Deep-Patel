using System;
using System.Collections.Generic;

namespace MyExtension
{
    public static class Class1
    {
        /// <summary>
        /// Print the list elements
        /// </summary>
        /// <param name="list"></param>
        public static void Print(this List<int> list)
        {
            Console.WriteLine("List elements ");

            foreach (int i in list) Console.Write(i + " ");

            Console.WriteLine();
        }
    }
}
