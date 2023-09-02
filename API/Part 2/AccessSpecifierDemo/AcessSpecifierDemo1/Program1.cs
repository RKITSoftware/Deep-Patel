using AccessSpecifierDemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcessSpecifierDemo1
{
    internal class Program1 : class1
    {
        static void Main(string[] args)
        {
            //class1 c1 = new class1();
            //c1.show1();

            Program1 p2 = new Program1();
            p2.show1();
        }
    }
}
