namespace ExtensionMEthodDemo
{
    static class NewMethodClass
    {
        /// <summary>
        /// Extension method of ExtDemoBaseClass
        /// </summary>
        /// <param name="ext">Binding Parameter for ExtDemoBaseClass</param>
        public static void M4(this ExtDemoBaseClass ext)
        {
            Console.WriteLine("This is method 4");
        }

        /// <summary>
        /// Extension method of DemoCheck
        /// </summary>
        /// <param name="ext">Binding Parameter for DemoCheck</param>
        /// <param name="str">string for written on console</param>
        public static void M5(this DemoCheck ext, string str)
        {
            Console.WriteLine(str);
        }

        /// <summary>
        /// Returns the count of list
        /// </summary>
        /// <param name="lst">List reference</param>
        /// <returns></returns>
        public static int M6(this List<int> lst)
        {
            return lst.Count;
        }
    }
}
