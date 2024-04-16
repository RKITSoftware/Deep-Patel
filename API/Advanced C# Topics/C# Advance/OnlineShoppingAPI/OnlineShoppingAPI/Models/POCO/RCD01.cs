using ServiceStack.DataAnnotations;
using System;

namespace OnlineShoppingAPI.Models.POCO
{
    /// <summary>
    /// Containing the order details of which customer by which product
    /// </summary>
    public class RCD01
    {
        /// <summary>
        /// Order Id
        /// </summary>
        [PrimaryKey]
        public int D01F01 { get; set; }

        /// <summary>
        /// Foreign key of Customer
        /// </summary>
        public int D01F02 { get; set; }

        /// <summary>
        /// Foreign key of Product
        /// </summary>
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

        /// <summary>
        /// Date and time when product bought.
        /// </summary>
        public string D01F07 { get; set; }
    }
}