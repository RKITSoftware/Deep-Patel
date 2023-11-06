using System;

namespace GenericsDemo
{
    /// <summary>
    /// For Generic Method Demonstartion
    /// </summary>
    class Example
    {
        #region Public Methods

        /// <summary>
        /// Prints the values of Array.
        /// </summary>
        /// <param name="myArr">Inout Array</param>
        public static void ShowArray<T>(T[] myArr)
        {
            for(int i = 0; i < myArr.Length; i++) 
            {
                Console.WriteLine(myArr[i]);
            }
        }

        public static bool Check<T>(T a, T b)
        {
            return a.Equals(b);
        }

        #endregion
    }

    /// <summary>
    /// For Generic Class Demonstartion
    /// </summary>
    class Example1<T>
    {
        #region Private Members

        private T box;

        #endregion

        #region Constructors

        public Example1(T a)
        {
            this.box = a;
        }

        #endregion

        #region Public Methods

        public T GetBox()
        {
            return this.box;
        }

        #endregion
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            
            int[] myNumbers = { 1, 2, 3, 4, 5, 6};
            string[] names = { "Deep", "Janvi", "Dhruvi" };
            double[] points = { 4.6, 4.67856, 23421.34325 };

            Example.ShowArray(myNumbers);
            Example.ShowArray(names);
            Example.ShowArray(points);

            Console.WriteLine(Example.Check(3, 6));
            Console.WriteLine(Example.Check("Deep", "Deep"));

            */

            Example1<int> e = new Example1<int>(10);
            Console.WriteLine(value: e.GetBox());

            Example1<string> e1 = new Example1<string>("Deep");
            Console.WriteLine(value: e1.GetBox());
        }
    }
}
