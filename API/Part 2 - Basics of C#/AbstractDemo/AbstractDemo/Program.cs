using System;

namespace AbstractDemo
{
    class Employee
    {
        #region Private Member

        double taxDeduction = 0.1;
        double netSalary;

        #endregion

        #region Public Members

        public int empId;
        public string empName;
        public double grossPay;

        #endregion

        #region Constructors
        public Employee(int empId, string empName, double grossPay)
        {
            this.empId = empId;
            this.empName = empName;
            this.grossPay = grossPay;
        }

        #endregion

        #region Public Methods
        public void ShowEmployeeDetails()
        {
            Console.WriteLine("Employee Id is {0}", this.empId);
            Console.WriteLine("Employee name is {0}", this.empName);
            this.CalculateSalary();
        }

        #endregion

        #region Private Methods
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

        #endregion
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
