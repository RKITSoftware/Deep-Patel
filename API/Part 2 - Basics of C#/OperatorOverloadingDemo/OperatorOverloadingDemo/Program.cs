using System;

namespace OperatorOverloadingDemo
{
    class NewClass
    {
        #region Public members

        public string str;
        public int num;

        #endregion

        #region Constructors
        public NewClass()
        {

        }

        public NewClass(string str, int num)
        {
            this.str = str;
            this.num = num;
        }

        #endregion

        #region Operator overloading
        public static NewClass operator +(NewClass obj1, NewClass obj2)
        {
            NewClass obj3 = new NewClass();
            obj3.str = obj1.str + " " + obj2.str;
            obj3.num = obj1.num + obj2.num;

            return obj3;
        }

        #endregion
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            NewClass obj1 = new NewClass("Deep", 25);
            NewClass obj2 = new NewClass("Janvi", 17);

            NewClass obj3 = obj1 + obj2;
            Console.WriteLine(obj3.str + " " + obj3.num);
        }
    }
}
