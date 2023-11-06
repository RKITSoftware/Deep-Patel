using System;

namespace StaticPropertyDemo
{
    class University
    {
        #region Private members

        private static string UniversityName;
        private static string DepartmentName;

        #endregion

        #region Public Properties

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
                    UniversityName = value;
                }
            }
            get
            {
                return UniversityName;
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
                    DepartmentName = value;
                }
            }
            get
            {
                return DepartmentName;
            }
        }

        #endregion
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