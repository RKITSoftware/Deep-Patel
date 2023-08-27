using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInputArray
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Initializing array with user input

            Console.WriteLine("Enter the size of array");
            int size = int.Parse(Console.ReadLine());

            int[] userArray = new int[size];

            Console.WriteLine("\nEnter the elements of array");
            for (int i = 0; i < size; i++)
            {
                userArray[i] = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("\nPrinting the array.");
            foreach (int item in userArray)
            {
                Console.Write(item + " ");
            }
        }
    }
}
