using System;

namespace MultipleInheritanceDemo
{
    class Person
    {
        #region Public methods
        public void Show()
        {
            Console.WriteLine("This is a method of person class");
        }

        #endregion
    }

    interface Employee
    {
        #region Public methods
        void Show();

        #endregion
    }

    class Teacher : Person, Employee
    {
        #region Public methods

        public new void Show()
        {
            Console.WriteLine("This is a method of person class.");
        }

        void Employee.Show()
        {
            Console.WriteLine("This is a method of employee interface");
        }

        #endregion
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Teacher t = new Teacher();
            t.Show();
            ((Employee)t).Show();

            Employee e = new Teacher();
            e.Show();
        }
    }
}
