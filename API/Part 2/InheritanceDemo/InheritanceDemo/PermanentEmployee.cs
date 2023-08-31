using System;

namespace InheritanceDemo
{
    // Derived Class
    internal class PermanentEmployee : Employee
    {
        public int permanentSalary;
        public int permanentHours;

        public PermanentEmployee (int empId, string empName, int empAge, int empContactNo, int permanentSalary, int permanentHours) 
            : base(empId, empName, empAge, empContactNo)
        {
            this.permanentHours = permanentHours;
            this.permanentSalary = permanentSalary;
        }
    }
}
