using System.ComponentModel.DataAnnotations;

namespace APILeraningModel.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Correct Title")]
        [MaxLength(30, ErrorMessage = "The length should be less than 30")]
        [MinLength(3, ErrorMessage = "The length should be greater than 3")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Enter Correct Category")]
        [MaxLength(30, ErrorMessage = "The length should be less than 30")]
        [MinLength(3, ErrorMessage = "The length should be greater than 3")]
        public string CategoryName { get; set; }
    }
}
