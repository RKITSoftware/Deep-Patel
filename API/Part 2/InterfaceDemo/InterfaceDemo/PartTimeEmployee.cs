using System;

namespace InterfaceDemo
{
    internal class PartTimeEmployee : IEmployee
    {
        public void Show()
        {
            Console.WriteLine("This is a method of IEmployee Interface");
        }
    }
}
