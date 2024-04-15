using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace OnlineShoppingAPI.Models.DTO
{
    /// <summary>
    /// DTO for PRO01 Model.
    /// </summary>
    public class DTOPRO01
    {
        /// <summary>
        /// Product Id
        /// </summary>
        [Required(ErrorMessage = "Id is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Id can't be negative.")]
        [JsonProperty("O01101")]
        public int O01F01 { get; set; }

        /// <summary>
        /// Product Name
        /// </summary>
        [Required(ErrorMessage = "Name is required.")]
        [JsonProperty("O01102")]
        public string O01F02 { get; set; }

        /// <summary>
        /// Product Price
        /// </summary>
        [JsonProperty("O01103")]
        public int O01F03 { get; set; }

        /// <summary>
        /// Product Quantity
        /// </summary>
        [JsonProperty("O01104")]
        public decimal O01104 { get; set; }

        /// <summary>
        /// Product Image
        /// </summary>
        [JsonProperty("O01105")]
        public string O01F05 { get; set; }

        /// <summary>
        /// Foreign Key of Suplier Table
        /// </summary>
        [Required(ErrorMessage = "Supplier id is required.")]
        [JsonProperty("O01106")]
        public int O01F06 { get; set; }
    }
}