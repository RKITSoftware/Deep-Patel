using System;

namespace ParamsDemo
{
    internal class Program
    {
        public static int Addition(params int[] nums)
        {
            int sum = 0;

            foreach(int num in nums)
            {
                sum += num;
            }

            return sum;
        }

        static void Main(string[] args)
        {
            Console.WriteLine(Addition(10, 20, 30, 40));
        }
    }
}
