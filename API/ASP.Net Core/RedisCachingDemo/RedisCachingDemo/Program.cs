using System;

namespace RedisCachingDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 1000000; i++)
            {
                MyCache.Get().StringSet("Deep" + i, "Hello");
            }

            Console.WriteLine(MyCache.Get().StringGet("Deep"));
            Console.ReadLine();
        }
    }
}
