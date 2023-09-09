using System;

namespace EncapsulationDemo
{
    class Person
    {
        #region Private Members

        private string personName;
        private int personAge;

        #endregion

        #region Public Methods

        public void setName(string personName)
        {
            if (string.IsNullOrEmpty(personName) == true)
            {
                Console.WriteLine("Name is Required");
            }
            else
            {
                this.personName = personName;
            }
        }

        public void setAge(int personAge)
        {
            if (personAge > 0)
            {
                this.personAge = personAge;
            }
            else
            {
                Console.WriteLine("Age should not be negative nor zero.");
            }
        }

        public void getName()
        {
            if (string.IsNullOrEmpty(this.personName) == false)
            {
                Console.WriteLine("Your name is {0}", this.personName);
            }
        }

        public void getAge()
        {
            if (this.personAge > 0)
            {
                Console.WriteLine("Your age is {0}", this.personAge);
            }
        }

        #endregion
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Person Deep = new Person();
            Deep.setName("Deep");
            Deep.setAge(21);

            Deep.getName();
            Deep.getAge();
        }
    }
}
