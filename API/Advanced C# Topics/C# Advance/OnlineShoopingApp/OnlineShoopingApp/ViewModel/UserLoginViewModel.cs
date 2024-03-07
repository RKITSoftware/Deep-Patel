using System.ComponentModel.DataAnnotations;

namespace OnlineShoopingApp.ViewModel
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "Enter the username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Enter the password")]
        public string Password { get; set; }
    }
}