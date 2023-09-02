using System;

namespace StaticPropertyDemo
{
    class University
    {
        private static string universityName;
        private static string departmentName;

        public static string _universityName
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("You can not enter null or empty value in university name");
                }
                else
                {
                    universityName = value;
                }
            }
            get
            {
                return universityName;
            }
        }

        public static string _departmentName
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("You can not enter null or empty value in university name");
                }
                else
                {
                    departmentName = value;
                }
            }
            get
            {
                return departmentName;
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            University._universityName = "GTU";
            University._departmentName = "Computer";
            Console.WriteLine(University._universityName + " " + University._departmentName);
        }
    }
}