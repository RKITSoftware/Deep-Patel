using MyExtension;
using System.Collections.Generic;

namespace BLFeature
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>() { 10, 45, 23, 67 };
            
            // Extension method of list using MyExtension Library
            list.Print();
        }
    }
}
