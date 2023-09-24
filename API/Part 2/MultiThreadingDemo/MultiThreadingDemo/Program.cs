using System;
using System.Threading;

namespace MultiThreadingDemo
{
    internal class Program
    {
        public static void Func1()
        {
            for (int i = 1; i <= 50; i++)
            {
                Console.WriteLine(value: "Func1 :- " + i);
            }
        }

        public static void Func2()
        {
            for (int i = 1; i <= 50; i++)
            {
                Console.WriteLine(value: "Func2 :- " + i);
                
                // if(i == 25)
                // {
                //     Console.WriteLine("Thread is going to sleep for 8 seconds...");
                //     Thread.Sleep(8000);
                // }
            }
        }

        public static void Func3()
        {
            for (int i = 1; i <= 50; i++)
            {
                Console.WriteLine(value: "Func3 :- " + i);
            }
        }

        static void Main(string[] args)
        {
            // Thread t = Thread.CurrentThread;
            // t.Name = "Main Thread";
            // Console.WriteLine(value: "Current Executing thread is :- " + Thread.CurrentThread.Name);

            Thread t1 = new Thread(Func1);
            Thread t2 = new Thread(Func2);
            Thread t3 = new Thread(Func3);

            t1.Start();
            t2.Start();
            t3.Start();
        }
    }
}
