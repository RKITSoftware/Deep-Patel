using System;

namespace ClassDemo
{
    internal class Student
    {
        int rollNo;
        string name;
        int age;
        int standard;

        public Student()
        {
            this.rollNo = 0;
            this.name = "Unknown";
            this.age = 0;
            this.standard = 0;
            Console.WriteLine("Default Constructor Called");
        }

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
    }
}
