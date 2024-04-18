using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace OnlineShoppingAPI.Models.DTO
{
    /// <summary>
    /// DTO for PRO02 Model.
    /// </summary>
    public class DTOPRO02
    {
        /// <summary>
        /// Product Id
        /// </summary>
        [Required(ErrorMessage = "Id is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Id can't be negative.")]
        [JsonProperty("O01101")]
        public int O02F01 { get; set; }

        /// <summary>
        /// Product Name
        /// </summary>
        [Required(ErrorMessage = "Enter the product name.")]
        [JsonProperty("O02102")]
        public string O02F02 { get; set; }

        /// <summary>
        /// Product Buy Price
        /// </summary>
        [Required(ErrorMessage = "Enter the buy price.")]
        [JsonProperty("O02F03")]
        public int O02F03 { get; set; }

        /// <summary>
        /// Product Sell Price
        /// </summary>
        [JsonProperty("O02104")]
        public int O02F04 { get; set; }

        /// <summary>
        /// Product Quantity
        /// </summary>
        [Required]
        [JsonProperty("O02105")]
        public int O02F05 { get; set; }

        /// <summary>
        /// Product Image
        /// </summary>
        [JsonProperty("O02106")]
        public string O02F06 { get; set; }

        /// <summary>
        /// Foreign Key of Category Table
        /// </summary>
        [Required(ErrorMessage = "Category id is required.")]
        [JsonProperty("O02109")]
        public int O02F09 { get; set; }

        /// <summary>
        /// Foreign Key of Suplier Table
        /// </summary>
        [Required(ErrorMessage = "Supplier id is required.")]
        [JsonProperty("O02110")]
        public int O02F10 { get; set; }
    }
}