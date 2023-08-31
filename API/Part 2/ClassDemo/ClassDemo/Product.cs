using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDemo
{
    static class Product
    {
        public static int productId;
        public static string productName;
        public static int productPrice;

        static Product()
        {
            productId = 1;
            productName = "Handwash";
            productPrice = 100;
        }

        public static void getProductDetails()
        {
            Console.WriteLine("Product Id is :- {0}", productId);
            Console.WriteLine("Product Name is :- {0}", productName);
            Console.WriteLine("Product Price is :- {0}", productPrice);
        }
    }
}
