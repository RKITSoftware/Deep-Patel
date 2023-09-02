using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleInheritanceDemo
{
    class Person
    {
        public void Show()
        {
            Console.WriteLine("This is a method of person class");
        }
    }

    interface Employee
    {
        void Show();
    }

    class Teacher : Person, Employee
    {
        public new void Show()
        {
            Console.WriteLine("This is a method of person class.");
        }

        void Employee.Show()
        {
            Console.WriteLine("This is a method of employee interface");
        }
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
