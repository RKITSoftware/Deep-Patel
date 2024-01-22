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
            objDemoCheck.M5("Deep Patel");
        }
    }
}