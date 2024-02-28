using System.ComponentModel.DataAnnotations;

namespace MySQLConnectUsingEntityFramework.Models
{
    public class UserData
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Minimum length is 8.")]
        [MinLength(8, ErrorMessage = "Length must be greater than 8.")]
        [MaxLength(16, ErrorMessage = "Length must be less thand 16.")]
        [DataType(DataType.Password, ErrorMessage = "Incorrect")]
        public string Password { get; set; }
    }
}
