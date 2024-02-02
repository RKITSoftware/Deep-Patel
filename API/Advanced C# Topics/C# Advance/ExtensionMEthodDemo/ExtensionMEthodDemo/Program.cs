namespace ExtensionMEthodDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ExtDemoBaseClass objExtension = new ExtDemoBaseClass();
            
            objExtension.M1();
            objExtension.M2();
            objExtension.M3();
            objExtension.M4();

            DemoCheck objDemoCheck = new DemoCheck();
            
            // Extension method
            objDemoCheck.M5("Deep Patel");
            List<int> lstNumbers = new List<int>()
            {
                1, 2, 3, 4, 5, 6, 7, 8
            };

            Console.WriteLine(lstNumbers.M6());
        }
    }
}