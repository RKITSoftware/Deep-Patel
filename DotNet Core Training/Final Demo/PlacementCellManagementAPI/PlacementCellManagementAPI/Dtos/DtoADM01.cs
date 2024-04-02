using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PlacementCellManagementAPI.Dtos
{
    /// <summary>
    /// Data Transfer Object (DTO) for admin.
    /// </summary>
    public class DtoADM01
    {
        /// <summary>
        /// Admin First Name
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Name length must be less than 50.")]
        [JsonProperty("M01F02")]
        public string M01102 { get; set; }

        /// <summary>
        /// Admin Last Name
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Name length must be less than 50.")]
        [JsonProperty("M01F03")]
        public string M01103 { get; set; }

        /// <summary>
        /// Date of Birth
        /// </summary>
        [Required]
        [DataType(DataType.Date, ErrorMessage = "Enter the date in correct format.")]
        [JsonProperty("M01F04")]
        public DateTime M01104 { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        [StringLength(1, MinimumLength = 1, ErrorMessage = "M for Male and F For Female")]
        [JsonProperty("M01F05")]
        public string M01105 { get; set; }

        /// <summary>
        /// Admin's Username
        /// </summary>
        [JsonProperty("R01F02")]
        public string R01102 { get; set; }

        /// <summary>
        /// Admin's Email
        /// </summary>
        [DataType(DataType.EmailAddress)]
        [JsonProperty("R01F03")]
        public string R01103 { get; set; }

        /// <summary>
        /// Admin's Password
        /// </summary>
        [JsonProperty("R01F04")]
        public string R01104 { get; set; }
    }
}
