using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValueAndReferenceType
{
    struct Employee
    {
        public int Salary;
        public int age;
    }

    class Person
    {
        public string name;
        public int age;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // struct -- value type -- stack
            // class -- reference type -- heap

            /* 
            Employee e = new Employee();
            e.Salary = 5000;
            e.age = 21;

            Employee e1 = e;
            Employee e2 = e;

            e.age = 25;
            Console.WriteLine(e.age);
            Console.WriteLine(e1.age);
            */

            /*
            Person p = new Person();
            p.name = "Deep";
            p.age = 21;

            Person p1 = p;
            Person p2 = p;

            p.age = 25;
            Console.WriteLine(p.age);
            Console.WriteLine(p1.age);
            */

            /*
             * Value Type
             * - bool
             * - byte
             * - char 
             * - decimal
             * - double
             * - enum
             * - float
             * - int 
             * - long
             * - sbyte
             * - short
             * - struct
             * - uint
             * - ushort
             * - ulong
            */

            /*
             * Reference Type
             * - string
             * - all arrays, even if there elements are value types
             * - class
             * - object
             * - delegates
             * - interface
            */ 
        }
    }
}
