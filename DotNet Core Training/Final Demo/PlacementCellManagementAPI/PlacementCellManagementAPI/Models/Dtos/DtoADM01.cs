using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PlacementCellManagementAPI.Models.Dtos
{
    /// <summary>
    /// Data Transfer Object (DTO) for admin.
    /// </summary>
    public class DTOADM01
    {
        /// <summary>
        /// Admin First Name
        /// </summary>
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "Name length must be less than 50.")]
        [JsonProperty("M01102")]
        public string? M01F02 { get; set; }

        /// <summary>
        /// Admin Last Name
        /// </summary>
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Name length must be less than 50.")]
        [JsonProperty("M01103")]
        public string? M01F03 { get; set; }

        /// <summary>
        /// Date of Birth
        /// </summary>
        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date, ErrorMessage = "Enter the date in correct format.")]
        [JsonProperty("M01104")]
        public DateTime M01F04 { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        [StringLength(1, MinimumLength = 1, ErrorMessage = "M for Male and F For Female")]
        [JsonProperty("M01105")]
        public string? M01F05 { get; set; }

        /// <summary>
        /// Admin's Username
        /// </summary>
        [Required(ErrorMessage = "User name is required.")]
        [JsonProperty("R01102")]
        public string? R01F02 { get; set; }

        /// <summary>
        /// Admin's Email
        /// </summary>
        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress)]
        [JsonProperty("R01103")]
        public string? R01F03 { get; set; }

        /// <summary>
        /// Admin's Password
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [JsonProperty("R01104")]
        public string? R01F04 { get; set; }
    }
}