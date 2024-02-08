using ServiceStack.DataAnnotations;

namespace OnlineShoppingAPI.Models
{
    [Alias("PRO01")]
    public class PRO01
    {
        /// <summary>
        /// Product Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int O01F01 { get; set; }

        /// <summary>
        /// Product Name
        /// </summary>
        [Required]
        [StringLength(100)]
        public string O01F02 { get; set; }

        /// <summary>
        /// Product Price
        /// </summary>
        [Required]
        [DecimalLength(10, 2)]
        public int O01F03 { get; set; }

        /// <summary>
        /// Product Quantity
        /// </summary>
        public decimal O01F04 { get; set; }

        /// <summary>
        /// Product Image
        /// </summary>
        public string O01F05 { get; set; }

        /// <summary>
        /// Foreign Key of Suplier Table
        /// </summary>
        [References(typeof(SUP01))]
        public int O01F06 { get; set; }
    }
}