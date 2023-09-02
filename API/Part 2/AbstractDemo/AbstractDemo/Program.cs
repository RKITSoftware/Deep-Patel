using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractDemo
{
    class Employee
    {
        public int empId;
        public string empName;
        public double grossPay;
        double taxDeduction = 0.1;
        double netSalary;

        public Employee(int empId, string empName, double grossPay)
        {
            this.empId = empId;
            this.empName = empName;
            this.grossPay = grossPay;
        }

        private void CalculateSalary()
        {
            if (this.grossPay >= 30000)
            {
                this.netSalary = this.grossPay - (this.taxDeduction * this.grossPay);
            }
            else
            {
                this.netSalary = this.grossPay;
            }

            Console.WriteLine("Your salary is {0}", this.netSalary);
        }

        public void ShowEmployeeDetails()
        {
            Console.WriteLine("Employee Id is {0}", this.empId);
            Console.WriteLine("Employee name is {0}", this.empName);
            this.CalculateSalary();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Employee Deep = new Employee(1, "Deep", 50000);
            Deep.ShowEmployeeDetails();
        }
    }
}
