using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PlacementCellManagementAPI.Dtos
{
    /// <summary>
    /// Dto for STU01 Poco model
    /// </summary>
    public class DtoSTU01
    {
        /// <summary>
        /// Student Full Name
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Name length must be less than 50.")]
        [JsonProperty("U01F02")]
        public string U01102 { get; set; }

        /// <summary>
        /// Student Full Name
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Name length must be less than 50.")]
        [JsonProperty("U01F03")]
        public string U01103 { get; set; }

        /// <summary>
        /// Date of Birth
        /// </summary>
        [Required]
        [DataType(DataType.Date, ErrorMessage = "Enter the date in correct format.")]
        [JsonProperty("U01F04")]
        public DateTime U01104 { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        [StringLength(1, MinimumLength = 1, ErrorMessage = "M for Male and F For Female")]
        [JsonProperty("U01F05")]
        public string U01105 { get; set; }

        /// <summary>
        /// Student's Aadhar Card Number
        /// </summary>
        [Required]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "Aadhar card number's length is 12")]
        [JsonProperty("U01F06")]
        public string U01106 { get; set; }

        /// <summary>
        /// Student's Username
        /// </summary>
        [JsonProperty("R01F02")]
        public string R01102 { get; set; }

        /// <summary>
        /// Student's Email
        /// </summary>
        [DataType(DataType.EmailAddress)]
        [JsonProperty("R01F03")]
        public string R01103 { get; set; }

        /// <summary>
        /// Student's Password
        /// </summary>
        [JsonProperty("R01F04")]
        public string R01104 { get; set; }
    }
}
