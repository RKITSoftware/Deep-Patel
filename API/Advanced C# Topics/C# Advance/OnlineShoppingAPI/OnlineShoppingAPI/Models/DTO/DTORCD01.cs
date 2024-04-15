using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace OnlineShoppingAPI.Models.DTO
{
    /// <summary>
    /// DTO for RCD01 Model.
    /// </summary>
    public class DTORCD01
    {
        /// <summary>
        /// Foreign key of Customer
        /// </summary>
        [Required(ErrorMessage = "Customer id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Value need to be greater than 0.")]
        [JsonProperty("D01102")]
        public int D01F02 { get; set; }

        /// <summary>
        /// Foreign key of Product
        /// </summary>
        [Required(ErrorMessage = "Product id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Value need to be greater than 0.")]
        [JsonProperty("D01103")]
        public int D01F03 { get; set; }

        /// <summary>
        /// Quantity of product that customer want to by it.
        /// </summary>
        [Required(ErrorMessage = "Please specify quantity.")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity value can't be negative.")]
        [JsonProperty("D01104")]
        public int D01F04 { get; set; }
    }
}