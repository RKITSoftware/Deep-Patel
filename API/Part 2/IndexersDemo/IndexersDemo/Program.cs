using System;

namespace IndexersDemo
{
    class Employee
    {
        #region Private members

        private int[] age = new int[3];

        #endregion

        #region Public properties

        public int this[int index]
        {
            get
            {
                return age[index];
            }

            set
            {
                if (index >= 0 && index < age.Length)
                {
                    if (value > 0)
                    {
                        age[index] = value;
                    }
                    else
                    {
                        Console.WriteLine(value: "Value is invalid!");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Index");
                }
            }
        }

        public int this[int index, int i]
        {
            get 
            {
                return age[index];
            }
            set
            {
                age[index] = value + i;
            }
        }
        #endregion
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // What are Indexers?
            Employee emp = new Employee();
            emp[0] = 5;
            Console.WriteLine(value: emp[0]);

            emp[0, 5] = 5;
            Console.WriteLine(value: emp[0]);
        }
    }
}
