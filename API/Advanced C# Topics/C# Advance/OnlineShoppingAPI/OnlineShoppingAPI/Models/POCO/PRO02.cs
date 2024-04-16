using ServiceStack.DataAnnotations;
using System;

namespace OnlineShoppingAPI.Models.POCO
{
    /// <summary>
    /// PRO02 updated version of PRO01.
    /// </summary>
    public class PRO02
    {
        /// <summary>
        /// Product Id
        /// </summary>
        [PrimaryKey]
        public int O02F01 { get; set; }

        /// <summary>
        /// Product Name
        /// </summary>
        public string O02F02 { get; set; }

        /// <summary>
        /// Product Buy Price
        /// </summary>
        public int O02F03 { get; set; }

        /// <summary>
        /// Product Sell Price
        /// </summary>
        public int O02F04 { get; set; }

        /// <summary>
        /// Product Quantity
        /// </summary>
        public decimal O02F05 { get; set; }

        /// <summary>
        /// Product Image
        /// </summary>
        public string O02F06 { get; set; }

        /// <summary>
        /// Product Status
        /// </summary>
        public int O02F07 { get; set; }

        /// <summary>
        /// Product Creation Time
        /// </summary>
        public DateTime O02F08 { get; set; }

        /// <summary>
        /// Foreign Key of Category Table
        /// </summary>
        public int O02F09 { get; set; }

        /// <summary>
        /// Foreign Key of Suplier Table
        /// </summary>
        public int O02F10 { get; set; }
    }
}