using System;
using ClassDemoStudent;

namespace ClassDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Student Deep = new Student();
            Student Janvi = new Student("Janvi");

            // - Instance member have a seperate copy for each and every object of the class.
            // - Instance member belongs to object of the class.
            // - Instance or non-static members are invoked using objects of the class.

            // - Static member belongs to the class.
            // - We can define a class members as static using the static keyword.

            Deep.setRollno(1);
            Console.WriteLine("\nThe roll number of Deep is :- " + Deep.getRollNo());

            Deep.setStudent(2, "Deep Patel", 21, 12);
            Deep.getStudent();

            Janvi.setStudent(3, "Janvi", 20, 11);
            Janvi.getStudent();

            Console.WriteLine(Student.schoolName);
            Console.WriteLine(Student.getFees());
            Console.WriteLine(Student.getFeesAnnualIncrement(3000));

            Student Vishal = new Student(Deep);
            Vishal.setRollno(5);
            Vishal.getStudent();

            Product.getProductDetails();
        }
    }
}
