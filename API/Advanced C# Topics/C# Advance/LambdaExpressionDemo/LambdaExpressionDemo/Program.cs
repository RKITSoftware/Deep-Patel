namespace LambdaExpressionDemo
{
    class Program
    {
        /// <summary>
        /// Using Lambda Expression
        /// </summary>
        static void ExpressionLambdaDemo()
        {
            // List to store numbers
            List<int> numbers = new List<int>() {36, 71, 12,
                            15, 29, 18, 27, 17, 9, 34};

            // foreach loop to display the list
            Console.Write("The list : ");
            foreach (int value in numbers)
            {
                Console.Write("{0} ", value);
            }
            Console.WriteLine();

            // Using lambda expression
            // to calculate square of
            // each value in the list
            var square = numbers.Select(num => num * num);

            // foreach loop to display squares
            Console.Write("Squares : ");
            foreach (int value in square)
            {
                Console.Write("{0} ", value);
            }
            Console.WriteLine();

            // Using Lambda expression to
            // find all numbers in the list
            // divisible by 3
            List<int> divBy3 = numbers.FindAll(num => (num % 3) == 0);

            // foreach loop to display divBy3
            Console.Write("Numbers Divisible by 3 : ");
            foreach (int value in divBy3)
            {
                Console.Write("{0} ", value);
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Program.ExpressionLambdaDemo();
        }
    }
}