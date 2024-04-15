using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace OnlineShoppingAPI.Models.DTO
{
    /// <summary>
    /// DTO for CAT01 Model
    /// </summary>
    public class DTOCAT01
    {
        /// <summary>
        /// Category Id
        /// </summary>
        [Required(ErrorMessage = "Category id is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Id can't be negative.")]
        [JsonProperty("T01101")]
        public int T01F01 { get; set; }

        /// <summary>
        /// Category Name
        /// </summary>
        [Required(ErrorMessage = "Category name is required.")]
        [JsonProperty("T01102")]
        public string T01F02 { get; set; }
    }
}