using Newtonsoft.Json;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace OnlineShoppingAPI.Models.POCO
{
    [Alias("PRO01")]
    public class PRO01
    {
        /// <summary>
        /// Product Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        [JsonProperty("O01101")]
        public int O01F01 { get; set; }

        /// <summary>
        /// Product Name
        /// </summary>
        [Required]
        [StringLength(100)]
        [ValidateMaximumLength(100)]
        [JsonProperty("O01102")]
        public string O01F02 { get; set; }

        /// <summary>
        /// Product Price
        /// </summary>
        [Required]
        [DecimalLength(10, 2)]
        [JsonProperty("O01103")]
        public int O01F03 { get; set; }

        /// <summary>
        /// Product Quantity
        /// </summary>
        [JsonProperty("O01104")]
        public decimal O01F04 { get; set; }

        /// <summary>
        /// Product Image
        /// </summary>
        [JsonProperty("O01105")]
        public string O01F05 { get; set; }

        /// <summary>
        /// Foreign Key of Suplier Table
        /// </summary>
        [References(typeof(SUP01))]
        [JsonProperty("O01106")]
        public int O01F06 { get; set; }
    }
}