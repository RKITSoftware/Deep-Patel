using System;

namespace InheritanceDemo
{
    // Base Class
    internal class Employee
    {
        public int empId;
        public string empName;
        public int empAge;
        public int empContactNo;

        public Employee (int empId, string empName, int empAge,
            int empContactNo)
        {
            this.empId = empId;
            this.empName = empName;
            this.empAge = empAge;
            this.empContactNo = empContactNo;
        }

        public void show()
        {
            Console.WriteLine("This is a method of base class.");
        }
    }
}
