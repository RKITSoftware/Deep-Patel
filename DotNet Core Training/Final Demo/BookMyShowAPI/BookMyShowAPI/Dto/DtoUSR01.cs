using System.ComponentModel.DataAnnotations;

namespace BookMyShowAPI.Dto
{
    /// <summary>
    /// Data transfer object model for User
    /// </summary>
    public class DtoUSR01
    {
        /// <summary>
        /// User's full name
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Length must be between 5 to 50 characters.")]
        public string R01101 { get; set; }

        /// <summary>
        /// User's username
        /// </summary>
        [Required]
        public string R01102 { get; set; }

        /// <summary>
        /// User's password
        /// </summary>
        [Required]
        [DataType(DataType.Password, ErrorMessage = "Password is invalid form.")]
        public string R01103 { get; set; }

        /// <summary>
        /// User's Confirm Password
        /// </summary>
        [Required]
        [Compare("R01103", ErrorMessage = "Password doesn't match")]
        public string R01104 { get; set; }
    }
}
