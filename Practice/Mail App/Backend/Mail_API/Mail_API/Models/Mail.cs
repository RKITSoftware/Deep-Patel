using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mail_API.Models
{
    public class Mail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Sender")]
        public int SenderId { get; set; }

        [Required]
        [ForeignKey("Receiver")]
        public int ReceiverId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime SentDate { get; set; } = DateTime.UtcNow;

        [DataType(DataType.DateTime)]
        public DateTime? ReadDate { get; set; }

        public User Sender { get; set; }
        public User Receiver { get; set; }
    }
}
