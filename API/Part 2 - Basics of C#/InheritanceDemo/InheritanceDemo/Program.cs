using System;

namespace InheritanceDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PermanentEmployee deep = new PermanentEmployee();
            deep.empId = 101;

            VisitingEmployee vishal = new VisitingEmployee();
            vishal.empId = 201;

            Console.WriteLine(deep.empId);
            Console.WriteLine(vishal.empId);

            deep.show();

            // 4 Types of Inheritance
            // - Single
            // - Hierarchical
            // - Multi-level
            // - Multiple (using interface)

            // 4 Pillars of OOPs
            // - Inheritance, Polymorphism, Encapsulation, Abstraction
        }
    }
}
