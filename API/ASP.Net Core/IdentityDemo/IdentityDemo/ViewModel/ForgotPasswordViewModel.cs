using System.ComponentModel.DataAnnotations;

namespace IdentityDemo.ViewModel
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
