using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstarctClassDemo
{
    abstract class Person
    {
        public abstract int Id { get; set; }
        public string firstName;
        public string lastName;
        public int age;
        public string phoneNumber;

        public abstract void PrintDetails();
    }

    class Student : Person
    {
        public int rollNo;
        public int fees;

        public override int Id
        {
            set
            {
                this.Id = value;
            }
            get
            {
                return this.Id;
            }
        }

        public override void PrintDetails()
        {
            string name = this.firstName + " " + this.lastName;
            Console.WriteLine("Student name is {0}", name);
            Console.WriteLine("Student age is {0}", this.age);
            Console.WriteLine("Student phone number is {0}", this.phoneNumber);
            Console.WriteLine("Student roll number is {0}", this.rollNo);
            Console.WriteLine("Student fee is {0}", this.fees);
        }
    }

    class Teacher : Person
    {
        public string qualificaation;
        public int salary;

        public override int Id
        {
            set
            {
                this.Id = value;
            }
            get
            {
                return this.Id;
            }
        }

        public override void PrintDetails()
        {
            string name = this.firstName + " " + this.lastName;
            Console.WriteLine("Teacher name is {0}", name);
            Console.WriteLine("Teacher age is {0}", this.age);
            Console.WriteLine("Teacher phone number is {0}", this.phoneNumber);
            Console.WriteLine("Teacher qualification number is {0}", this.qualificaation);
            Console.WriteLine("Teacher salary is {0}", this.salary);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Student Deep = new Student();
            Deep.firstName = "Deep";
            Deep.lastName = "Patel";
            Deep.age = 21;
            Deep.phoneNumber = "9909583015";
            Deep.rollNo = 25;
            Deep.fees = 100000;

            Deep.PrintDetails();
            Console.WriteLine();

            Teacher Shyam = new Teacher();
            Shyam.firstName = "Shyam";
            Shyam.lastName = "Kotecha";
            Shyam.age = 35;
            Shyam.phoneNumber = "7757528684";
            Shyam.qualificaation = "Master in Computer Science";
            Shyam.salary = 80000;

            Shyam.PrintDetails();
        }
    }
}
