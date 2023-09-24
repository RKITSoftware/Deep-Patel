using System;

namespace PartialClassDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StudentPartial obj = new StudentPartial
            {
                FirstName = "Deep",
                LastName = "Patel"
            };

            Console.WriteLine("Your Complete name is :- " + obj.GetCompleteName());
        }
    }
}
