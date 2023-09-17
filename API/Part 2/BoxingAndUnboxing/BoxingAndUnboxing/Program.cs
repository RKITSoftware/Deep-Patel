using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxingAndUnboxing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int num1 = 10; // value type
            object obj1 = num1; // implicit conversion from value type to reference type
            int num2 = (int)obj1;
        }
    }
}
