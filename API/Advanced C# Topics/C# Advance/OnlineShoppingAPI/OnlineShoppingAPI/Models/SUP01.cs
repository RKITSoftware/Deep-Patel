using ServiceStack.DataAnnotations;

namespace OnlineShoppingAPI.Models
{
    public class SUP01
    {
        /// <summary>
        /// Suplier Id
        /// </summary>
        [AutoIncrement]
        public int P01F01 { get; set; }

        /// <summary>
        /// Suplier name
        /// </summary>
        [Required]
        public string P01F02 { get; set; }

        /// <summary>
        /// Suplier Gmail Id
        /// </summary>
        [Required]
        public string P01F03 { get; set; }

        /// <summary>
        /// Suplier password
        /// </summary>
        [Required]
        public string P01F04 { get; set; }

        /// <summary>
        /// Suplier Mobile Number
        /// </summary>
        public string P01F05 { get; set; }

        /// <summary>
        /// Suplier GST Number
        /// </summary>
        [StringLength(maximumLength: 15, minimumLength: 15)]
        public string P01F06 { get; set; }
    }
}