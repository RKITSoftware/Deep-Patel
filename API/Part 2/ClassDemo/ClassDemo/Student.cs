using System;

namespace ClassDemoStudent
{
    internal class Student
    {
        #region Private Members

        int rollNo;
        string name;
        int age;
        int standard;

        #endregion

        #region Static Members

        public static string schoolName;
        public static int fees;

        #endregion

        #region Constuctor

        // Static Constructor
        static Student()
        {
            schoolName = "Nilkanth Vidhyalay";
            fees = 5000;
            Console.WriteLine("Static Constructor Invoked");
        }

        // Default Constructor
        public Student()
        {
            this.rollNo = 0;
            this.name = "Unknown";
            this.age = 0;
            this.standard = 0;
            Console.WriteLine("Default Constructor Called");
        }

        // Parameterized Constructor
        public Student(string name)
        {
            this.rollNo = 0;
            this.name = name;
            this.age = 0;
            this.standard = 0;
            Console.WriteLine("Single parameter constructor called");
        }

        public Student(int rollNo, string name, int age, int standard)
        {
            this.rollNo = rollNo;
            this.name = name;
            this.age = age;
            this.standard = standard;
            Console.WriteLine("Multiple parameter constructor called");
        }

        // Copy Constructor
        public Student(Student stu)
        {
            this.rollNo = stu.rollNo;
            this.name = stu.name;
            this.age = stu.age;
            this.standard = stu.standard;
            Console.WriteLine("Copy Constructor called");
        }

        #endregion

        #region Destructor

        ~Student()
        {
            Console.WriteLine("Destructor has been invoked");
        }

        #endregion

        #region Public Methods
        public void setRollno(int rollNo)
        {
            this.rollNo = rollNo;
        }

        public void setStudent(int rollNo, string name, int age, int standard)
        {
            this.rollNo = rollNo;
            this.name = name;
            this.age = age;
            this.standard = standard;
        }

        public int getRollNo()
        {
            return this.rollNo;
        }

        public void getStudent()
        {
            Console.WriteLine("Student roll number is :- {0}", this.rollNo);
            Console.WriteLine("Student name is :- {0}", this.name);
            Console.WriteLine("Student age is :- {0}", this.age);
            Console.WriteLine("Student standard is :- {0}\n", this.standard);
        }

        #endregion

        #region Static Methods
        public static int getFees()
        {
            return fees;
        }

        public static int getFeesAnnualIncrement(int fee)
        {
            return fee / 10;
        }

        #endregion
    }
}
