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
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Name length must be between 5 to 50.")]
        public string U01101 { get; set; }

        /// <summary>
        /// Date of Birth
        /// </summary>
        [Required]
        [DataType(DataType.Date, ErrorMessage = "Enter the date in correct format.")]
        public DateTime U01102 { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        [StringLength(1, MinimumLength = 1, ErrorMessage = "M for Male and F For Female")]
        public string U01103 { get; set; }

        /// <summary>
        /// Student's Aadhar Card Number
        /// </summary>
        [Required]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "Aadhar card number's length is 12")]
        public string U01104 { get; set; }

        /// <summary>
        /// Student's Username
        /// </summary>
        public string U01105 { get; set; }

        /// <summary>
        /// Student's Email
        /// </summary>
        [DataType(DataType.EmailAddress)]
        public string U01106 { get; set; }

        /// <summary>
        /// Student's Password
        /// </summary>
        public string U01107 { get; set; }
    }
}
