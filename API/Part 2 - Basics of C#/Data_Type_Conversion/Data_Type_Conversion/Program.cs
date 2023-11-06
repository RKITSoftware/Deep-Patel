using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Type_Conversion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Data Type Conversion

            int a = 20;

            float b = a; // Implicit Data Type Conversion
            Console.WriteLine(b);

            float c = 21.56f;
            int d = (int)c; // Explicit Data Type Conversion
            int e = Convert.ToInt32(c);
            Console.WriteLine("{0} {1}", d, e);

            string f = "50.123";
            string g = "60";

            
            // int h = Convert.ToInt32(f) + Convert.ToInt32(g);
            float i = float.Parse(f) + int.Parse(g);
            Console.WriteLine(i);

            Console.ReadLine();
        }
    }
}
