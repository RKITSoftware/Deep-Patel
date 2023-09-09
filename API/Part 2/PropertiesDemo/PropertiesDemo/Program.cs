using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertiesDemo
{
    class Student
    {
        #region Private members

        private int _stdId;
        private string _studentName;
        private string _fatherName;
        private int _subjectTotalMarks = 100;

        #endregion

        #region Public properties
        public int percentage { get; private set; }

        public int stdId
        {
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine("Id can not be negative or zero.");
                }
                else
                {
                    this._stdId = value;
                    this.percentage = 10;
                }
            }
            get
            {
                return this._stdId;
            }
        }

        public string studentName
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("Please enter your name");
                }
                else
                {
                    this._studentName = value;
                }
            }
            get
            {
                return this._studentName;
            }
        }

        public string fatherName
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("Please enter your name");
                }
                else
                {
                    this._fatherName = value;
                }
            }
            get
            {
                return this._fatherName;
            }
        }

        public int subjectTotalMarks
        {
            get
            {
                return this._subjectTotalMarks;
            }
        }

        #endregion
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Student Deep = new Student();
            Deep.stdId = 1;
            Deep.studentName = "Deep";
            Deep.fatherName = "Rameshbhai";
            Console.WriteLine(Deep.stdId + " " + Deep.studentName + " " + Deep.fatherName 
                + " " + Deep.subjectTotalMarks + " " + Deep.percentage);
        }
    }
}
