using System;

namespace InheritanceDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PermanentEmployee Deep = new PermanentEmployee();
            Deep.empId = 101;

            VisitingEmployee Vishal = new VisitingEmployee();
            Vishal.empId = 201;

            Console.WriteLine(Deep.empId);
            Console.WriteLine(Vishal.empId);

            Deep.show();

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
