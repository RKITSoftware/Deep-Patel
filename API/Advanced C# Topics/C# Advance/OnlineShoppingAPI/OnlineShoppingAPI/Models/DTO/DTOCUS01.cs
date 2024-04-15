using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace OnlineShoppingAPI.Models.DTO
{
    /// <summary>
    /// DTO for CUS01 Model
    /// </summary>
    public class DTOCUS01
    {
        /// <summary>
        /// Customer Id
        /// </summary>
        [Required(ErrorMessage = "Id is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Id's value can't be negative.")]
        [JsonProperty("S01101")]
        public int S01F01 { get; set; }

        /// <summary>
        /// Customer Name
        /// </summary>
        [Required(ErrorMessage = "Name is required.")]
        [JsonProperty("S01102")]
        public string S01F02 { get; set; }

        /// <summary>
        /// Customer Email Address
        /// </summary>
        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is invalid.")]
        [JsonProperty("S01103")]
        public string S01F03 { get; set; }

        /// <summary>
        /// Customer Password
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password, ErrorMessage = "Password is in incorrect form.")]
        [JsonProperty("S01104")]
        public string S01F04 { get; set; }

        /// <summary>
        /// Customer Mobile Number
        /// </summary>
        [Required(ErrorMessage = "Mobile number is required.")]
        [DataType(DataType.PhoneNumber)]
        [JsonProperty("S01105")]
        public string S01F05 { get; set; }

        /// <summary>
        /// Customer Address
        /// </summary>
        [JsonProperty("S01106")]
        public string S01F06 { get; set; }
    }
}