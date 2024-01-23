namespace LinqListDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Creating a list of numbers
            List<int> lstNumbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

            // Filter the lstNumbers and return all even numbers.
            List<int> lstEvenNumbers = lstNumbers.Where(num => num % 2 == 0).ToList();

            // Ordering Operators
            // Sorting the numbers on lstEvenNumbers
            lstEvenNumbers = lstEvenNumbers.OrderBy(num => num).ToList();

            // Grouping the numbers which is modulo of 4
            var groups = lstNumbers.GroupBy(num => num % 4);

            // Key value
            foreach(var item in groups)
            {
                Console.WriteLine("Key is :- " + item.Key + " ");
                
                foreach (var number in item)
                    Console.Write(number + " ");

                Console.WriteLine();
            }

            // Creating a new list with prepend a 1 number to the lstNumbers.
            var lstTemp = lstNumbers.Prepend(1);

            // Checking 1 exists or not in list.
            Console.WriteLine(lstTemp.Contains(1));

            // Sum
            Console.WriteLine("sum of list is :- " + lstNumbers.Sum());

            // Partitioning Operators
            foreach(var number in lstTemp.Take(1))
            {
                Console.WriteLine(number + " ");
            }

            // Element Operators
            Console.WriteLine("First Element :- " + lstEvenNumbers.First());
            Console.WriteLine("Last Element :- " + lstEvenNumbers.Last());
            // Console.WriteLine("Single Element :- " + lstEvenNumbers.SingleOrDefault());
            Console.WriteLine("Element at 4th position :- " + lstEvenNumbers.ElementAt(4));
        }
    }
}