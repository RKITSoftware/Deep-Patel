using System;

namespace ClassDemo
{
    static class Product
    {
        #region Static Members

        public static int productId;
        public static string productName;
        public static int productPrice;

        #endregion

        #region Constructor

        static Product()
        {
            productId = 1;
            productName = "Handwash";
            productPrice = 100;
        }

        #endregion

        #region Public Methods
        public static void getProductDetails()
        {
            Console.WriteLine("Product Id is :- {0}", productId);
            Console.WriteLine("Product Name is :- {0}", productName);
            Console.WriteLine("Product Price is :- {0}", productPrice);
        }

        #endregion
    }
}
