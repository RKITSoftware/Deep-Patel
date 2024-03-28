using System.ComponentModel.DataAnnotations;

namespace PlacementCellManagementAPI.Dtos
{
    /// <summary>
    /// Data Transfer Object (DTO) for admin.
    /// </summary>
    public class DtoADM01
    {
        /// <summary>
        /// Admin Full Name
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Name length must be between 5 to 50.")]
        public string M01101 { get; set; }

        /// <summary>
        /// Date of Birth
        /// </summary>
        [Required]
        [DataType(DataType.Date, ErrorMessage = "Enter the date in correct format.")]
        public DateTime M01102 { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        [StringLength(1, MinimumLength = 1, ErrorMessage = "M for Male and F For Female")]
        public string M01103 { get; set; }

        /// <summary>
        /// Admin's Username
        /// </summary>
        public string M01104 { get; set; }

        /// <summary>
        /// Admin's Email
        /// </summary>
        [DataType(DataType.EmailAddress)]
        public string M01105 { get; set; }

        /// <summary>
        /// Admin's Password
        /// </summary>
        public string M01106 { get; set; }
    }
}
