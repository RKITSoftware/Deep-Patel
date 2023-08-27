using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeOfArrays
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Types of Arrays
            // - Single-Dimentional Arrays
            // - Multi-Dimentional Arrays

            // Single
            // int[] arr = new int[2];
            // arr[0] = 10;
            // arr[1] = 20;

            // Multi-Dimentional
            // - A multi dimentional array allows you tostore combination of values
            //   of a single type in two or more dimensions.

            // 3 Ways to create Multi Dimensional Array
            // int[,] arr = new int[3,3] { { 1, 2, 3}, { 4, 5, 6}, { 7, 8, 9} };
            // int[,] arr = new int[,] { { 1, 2, 3}, { 4, 5, 6}, { 7, 8, 9} };
            // int[,] arr = { { 1, 2, 3}, { 4, 5, 6}, { 7, 8, 9} };

            /*
            int[,] myarr2 = new int[3, 4] 
            { 
                { 1, 2, 3, 10}, 
                { 4, 5, 6, 11}, 
                { 7, 8, 9, 12} 
            };

            Console.WriteLine(myarr2[2, 2]);
            Console.WriteLine(myarr2.GetLength(0));
            Console.WriteLine(myarr2.GetLength(1));
            Console.WriteLine(myarr2.Rank + "\n");

            for(int i = 0; i < myarr2.GetLength(0); i++)
            {
                for(int j = 0; j < myarr2.GetLength(1); j++)
                {
                    Console.Write(myarr2[i, j] + " ");
                }
                Console.WriteLine();
            }

            foreach(int item in myarr2)
            {
                Console.WriteLine(item);
            }
            */

            // Jagged Array
            // - can have unequal number of columns for each row

            int[][] myArr3 = new int[3][];
            myArr3[0] = new[] { 1, 3, 5, 24, 6, 67, 34, 78, 4};
            myArr3[1] = new[] { 1, 3, 5, 67, 34, 78, 4};
            myArr3[2] = new[] { 1, 3, 5, 34, 6, 22, 11, 24, 6, 67, 34, 78, 4};

            Console.WriteLine(myArr3.GetLength(0) + "\n");

            for(int i = 0; i < myArr3.Length; i++)
            {
                for(int j = 0; j < myArr3[i].Length; j++)
                {
                    Console.Write(myArr3[i][j] + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nForEach \n");
            foreach (int[] items in myArr3)
            {
                foreach(int item in items)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
