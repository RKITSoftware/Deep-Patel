namespace DyanmicTypeDemo
{
    internal class Program
    {
        /// <summary>
        /// Add two parameters
        /// </summary>
        /// <param name="temp1"></param>
        /// <param name="temp2"></param>
        static void Add(dynamic temp1, dynamic temp2)
        {
            Console.WriteLine(temp1 + temp2);
        }

        static void Main(string[] args)
        {
            /*
             * - dynamic was introduced in C# 4.0
             * - dynamic keyword is also used to store any type of data in its variable.
             * - value of dynamic variable is defined at run time.
             * - Initialization is not mandatory when we declare a variable with dynamic keyword.
             * - GetType() returns type of value stored in dynamic keyword.
             * - we can change the value of dynamic keyword
             * - Dynamic keyword does not support intellisense.
             * - dynamic variables can be used to create properties and return values from a function
             * - we can use dynamic variable as a function parameter
             * - dynamic keyword is of reference type
            */

            dynamic user = "Deep Patel";
            dynamic a;
            a = 25;
            Console.WriteLine("Before :- " + a.GetType());
            a = 'D';
            Console.WriteLine("After :- " + a.GetType() + "\n");

            // Dynamic Methods
            Add("Deep ", "Patel");
            Add(25, 'z');
            Add('A', 'B');
            Add(34.655, 32.77788);
        }
    }
}