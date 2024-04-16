using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PlacementCellManagementAPI.Models.Dtos
{
    /// <summary>
    /// DTO for STU01 POCO model.
    /// </summary>
    public class DTOSTU01
    {
        /// <summary>
        /// Gets or sets the Student's First Name.
        /// </summary>
        [Required(ErrorMessage = "Student's first name is required.")]
        [StringLength(50, ErrorMessage = "Name length must be less than or equal to 50 characters.")]
        [JsonProperty("U01102")]
        public string? U01F02 { get; set; }

        /// <summary>
        /// Gets or sets the Student's Last Name.
        /// </summary>
        [Required(ErrorMessage = "Student's last name is required.")]
        [StringLength(50, ErrorMessage = "Name length must be less than or equal to 50 characters.")]
        [JsonProperty("U01103")]
        public string? U01F03 { get; set; }

        /// <summary>
        /// Gets or sets the Date of Birth.
        /// </summary>
        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date, ErrorMessage = "Enter the date in correct format.")]
        [JsonProperty("U01104")]
        public DateTime U01F04 { get; set; }

        /// <summary>
        /// Gets or sets the Gender.
        /// </summary>
        [StringLength(1, MinimumLength = 1, ErrorMessage = "Gender must be 'M' for Male and 'F' for Female.")]
        [JsonProperty("U01105")]
        public string? U01F05 { get; set; }

        /// <summary>
        /// Gets or sets the Student's Aadhar Card Number.
        /// </summary>
        [Required(ErrorMessage = "Aadhar card number is required.")]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "Aadhar card number's length must be 12 characters.")]
        [JsonProperty("U01106")]
        public string? U01F06 { get; set; }

        /// <summary>
        /// Gets or sets the Student's Username.
        /// </summary>
        [Required(ErrorMessage = "Username is required.")]
        [JsonProperty("R01102")]
        public string? R01F02 { get; set; }

        /// <summary>
        /// Gets or sets the Student's Email.
        /// </summary>
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        [JsonProperty("R01103")]
        public string? R01F03 { get; set; }

        /// <summary>
        /// Gets or sets the Student's Password.
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        [JsonProperty("R01104")]
        public string? R01F04 { get; set; }
    }
}
