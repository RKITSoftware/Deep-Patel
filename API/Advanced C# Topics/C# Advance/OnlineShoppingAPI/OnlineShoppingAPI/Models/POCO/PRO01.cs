using ServiceStack;
using ServiceStack.DataAnnotations;
using System.Text.Json.Serialization;

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
        [JsonPropertyName("O01101")]
        public int O01F01 { get; set; }

        /// <summary>
        /// Product Name
        /// </summary>
        [Required]
        [StringLength(100)]
        [ValidateMaximumLength(100)]
        [JsonPropertyName("O01102")]
        public string O01F02 { get; set; }

        /// <summary>
        /// Product Price
        /// </summary>
        [Required]
        [DecimalLength(10, 2)]
        [JsonPropertyName("O01103")]
        public int O01F03 { get; set; }

        /// <summary>
        /// Product Quantity
        /// </summary>
        [JsonPropertyName("O01104")]
        public decimal O01F04 { get; set; }

        /// <summary>
        /// Product Image
        /// </summary>
        [JsonPropertyName("O01105")]
        public string O01F05 { get; set; }

        /// <summary>
        /// Foreign Key of Suplier Table
        /// </summary>
        [References(typeof(SUP01))]
        [JsonPropertyName("O01106")]
        public int O01F06 { get; set; }
    }
}