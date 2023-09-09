using System;

namespace AbstarctClassDemo
{
    abstract class Person
    {
        #region Public members
        
        public string firstName;
        public string lastName;
        public int age;
        public string phoneNumber;

        #endregion

        #region Public Properties
        public abstract int Id { get; set; }

        #endregion

        #region Public Methods
        public abstract void PrintDetails();

        #endregion
    }

    class Student : Person
    {
        #region Public members

        public int rollNo;
        public int fees;

        #endregion

        #region Public Properties
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

        #endregion

        #region Public Methods
        public override void PrintDetails()
        {
            string name = this.firstName + " " + this.lastName;
            Console.WriteLine("Student name is {0}", name);
            Console.WriteLine("Student age is {0}", this.age);
            Console.WriteLine("Student phone number is {0}", this.phoneNumber);
            Console.WriteLine("Student roll number is {0}", this.rollNo);
            Console.WriteLine("Student fee is {0}", this.fees);
        }

        #endregion
    }

    class Teacher : Person
    {
        #region Public members

        public string qualificaation;
        public int salary;

        #endregion

        #region Public Properties
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

        #endregion

        #region Public Methods
        public override void PrintDetails()
        {
            string name = this.firstName + " " + this.lastName;
            Console.WriteLine("Teacher name is {0}", name);
            Console.WriteLine("Teacher age is {0}", this.age);
            Console.WriteLine("Teacher phone number is {0}", this.phoneNumber);
            Console.WriteLine("Teacher qualification number is {0}", this.qualificaation);
            Console.WriteLine("Teacher salary is {0}", this.salary);
        }

        #endregion
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Student deep = new Student();
            deep.firstName = "Deep";
            deep.lastName = "Patel";
            deep.age = 21;
            deep.phoneNumber = "9909583015";
            deep.rollNo = 25;
            deep.fees = 100000;

            deep.PrintDetails();
            Console.WriteLine();

            Teacher shyam = new Teacher();
            shyam.firstName = "Shyam";
            shyam.lastName = "Kotecha";
            shyam.age = 35;
            shyam.phoneNumber = "7757528684";
            shyam.qualificaation = "Master in Computer Science";
            shyam.salary = 80000;

            shyam.PrintDetails();
        }
    }
}
