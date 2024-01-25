using ServiceStack.DataAnnotations;
using System;

namespace OnlineShoppingAPI.Models
{
    /// <summary>
    /// Containing the order details of which customer by which product
    /// </summary>
    [Alias("RCD01")]
    public class RCD01
    {
        /// <summary>
        /// Order Id
        /// </summary>
        [AutoIncrement]
        public int D01F01 { get; set; }

        /// <summary>
        /// Foreign key of Customer
        /// </summary>
        [References(typeof(CUS01))]
        public int D01F02 { get; set; }

        /// <summary>
        /// Foreign key of Product
        /// </summary>
        [References(typeof(PRO01))]
        public int D01F03 { get; set; }

        /// <summary>
        /// Quantity of product that customer want to by it.
        /// </summary>
        public int D01F04 { get; set; }

        /// <summary>
        /// Price of product
        /// </summary>
        public int D01F05 { get; set; }

        /// <summary>
        /// Invoice id for order
        /// </summary>
        public Guid D01F06 { get; set; }
    }
}