using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace OnlineShoppingAPI.Models.DTO
{
    /// <summary>
    /// DTO for ADM01 Model.
    /// </summary>
    public class DTOADM01
    {
        /// <summary>
        /// Admin Name
        /// </summary>
        [Required(ErrorMessage = "Name is required.")]
        [JsonProperty("M01102")]
        public string M01F02 { get; set; }

        /// <summary>
        /// Admin Email
        /// </summary>
        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is invalid.")]
        [JsonProperty("M01103")]
        public string M01F03 { get; set; }

        /// <summary>
        /// Admin Password
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password, ErrorMessage = "Password is in incorrect form.")]
        [JsonProperty("R01103")]
        public string R01F03 { get; set; }
    }
}