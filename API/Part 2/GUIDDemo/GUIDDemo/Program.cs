using System;

namespace GUIDDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Guid g = Guid.NewGuid();
            // Console.WriteLine(g.ToString().Replace("-", String.Empty));
            // Console.WriteLine(g.ToString("N"));
            Console.WriteLine(g.ToString("N").Substring(0, 15));
        }
    }
}
