using System.ComponentModel.DataAnnotations;

namespace Mail_API.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 6)]
        public string Name { get; set; }

        [StringLength(20, MinimumLength = 5)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Number { get; set; }

        [Required]
        [Range(1, 100)]
        public int Age { get; set; }
    }
}
