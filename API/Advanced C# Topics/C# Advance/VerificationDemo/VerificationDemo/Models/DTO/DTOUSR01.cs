using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace VerificationDemo.Models.DTO
{
    /// <summary>
    /// DTO for Create and Update Operation of USR01
    /// </summary>
    public class DTOUSR01
    {
        /// <summary>
        /// User Id
        /// </summary>
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Id must be positive.")]
        [JsonProperty("R01101")]
        public int R01F01 { get; set; }

        /// <summary>
        /// User's Name
        /// </summary>
        [Required(ErrorMessage = "Please enter name.")]
        [JsonProperty("R01102")]
        public string R01F02 { get; set; }

        /// <summary>
        /// User's Age
        /// </summary>
        [Range(0, 99, ErrorMessage = "Age can't be negative nor greater than 99.")]
        [JsonProperty("R01103")]
        public int R01F03 { get; set; }
    }
}