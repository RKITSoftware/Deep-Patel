using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Array_CSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            
            // An Array is a collection of elements of a single data type stored in adjacent memory loations.
            // - Simplifies the task of maintaining values.
            // - C# supports zero-based index values in an array
            // - Each value is refereed as elements.
            // - These Elements are accessed using subscripts or index numbers that determine the position
            //   of the element in the array list.
            // - This arrangment of storing values helps in efficient storage of data, easy sorting of data,
            //   and easy tracking of data length.

            // Array Declaration
            int[] myArr = new int[3];
            string[] myArr2 = new string[] { "Deep", "Vishal", "Yash"};
            string[] myArr3 = { "Deep", "Vishal", "Yash"};
            
            myArr[0] = 11;
            myArr[1] = 12;
            myArr[2] = 13;

            // Unhandled Exception: System.IndexOutOfRangeException: Index was outside the bounds of the array.
            // myArr[3] = 12;

            Console.WriteLine(myArr[2]);

            // get array Length
            Console.WriteLine("Array length is :- " + myArr.Length);

            // Arrays are reference type variables whose creation involves two steps:
            // - Declaration
            // - Memory Allocation
            */

            // For Each loop
            // - is used to perform specific actions on large data collections
            //   and can even be used on arrays.
            // - Allows you to execute a block of code for each element in the array.
            // - Is particularly use for reference type, such as strings.

            int[] numbers = new int[] { 1, 2, 3, 4 };
            foreach(int value in numbers)
            {
                Console.WriteLine(value);
            }
        }
    }
}
