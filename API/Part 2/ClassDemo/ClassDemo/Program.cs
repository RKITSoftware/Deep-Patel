using System;

namespace ClassDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Student Deep = new Student();
            Student Janvi = new Student("Janvi");

            Deep.setRollno(1);
            Console.WriteLine("\nThe roll number of Deep is :- " + Deep.getRollNo());

            Deep.setStudent(2, "Deep Patel", 21, 12);
            Deep.getStudent();

            Janvi.setStudent(3, "Janvi", 20, 11);
            Janvi.getStudent();
        }
    }
}
