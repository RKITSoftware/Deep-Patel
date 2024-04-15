using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace OnlineShoppingAPI.Models.DTO
{
    /// <summary>
    /// DTO for CRT01 Model
    /// </summary>
    public class DTOCRT01
    {
        /// <summary>
        /// Customer id foreign key for cart
        /// </summary>
        [Required(ErrorMessage = "Customer id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Id needs to be greater than zero.")]
        [JsonProperty("T01102")]
        public int T01F02 { get; set; }

        /// <summary>
        /// Product id foreign key for cart
        /// </summary>
        [Required(ErrorMessage = "Product id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Id needs to be greater than zero.")]
        [JsonProperty("T01103")]
        public int T01F03 { get; set; }

        /// <summary>
        /// Product Quantity that customer need
        /// </summary>
        [Required(ErrorMessage = "Customer id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity value needs to be greater than zero.")]
        [JsonProperty("T01104")]
        public int T01F04 { get; set; }
    }
}