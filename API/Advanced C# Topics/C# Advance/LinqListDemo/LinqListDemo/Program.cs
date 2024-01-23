namespace LinqListDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> lstNumbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

            List<int> lstEvenNumbers = lstNumbers.Where(num => num % 2 == 0).ToList();
            lstEvenNumbers = lstEvenNumbers.OrderBy(num => num).ToList();

            var groups = lstEvenNumbers.GroupBy(num => num % 4);

            foreach(var item in groups)
            {
                Console.WriteLine("Key is :- " + item.Key + " ");
                foreach(var number in item)
                {
                    Console.WriteLine(number);
                }
            }

            var lstTemp = lstNumbers.Prepend(1);
            Console.WriteLine(lstTemp.Contains(1));
            foreach(var item in lstTemp)
            {
                Console.WriteLine(item);
            }
        }
    }
}