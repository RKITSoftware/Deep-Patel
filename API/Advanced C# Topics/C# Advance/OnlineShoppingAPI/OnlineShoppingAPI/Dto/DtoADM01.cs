using System.ComponentModel.DataAnnotations;

namespace OnlineShoppingAPI.Dto
{
    public class DtoADM01
    {
        /// <summary>
        /// Admin Full Name
        /// </summary>
        [Required(ErrorMessage = "Admin full name is required")]
        [StringLength(50, MinimumLength = 4,
            ErrorMessage = "Admin full name length must be between 4 and 50 characters.")]
        public string M01101 { get; set; }

        /// <summary>
        /// Admin Email Id
        /// </summary>
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string M01102 { get; set; }

        /// <summary>
        /// Admin Password
        /// </summary>
        [Required(ErrorMessage = "Admin Password is required.")]
        [MinLength(6, ErrorMessage = "Admin Password length must be at least 6 characters.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,}$",
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string M01103 { get; set; }
    }
}