using ServiceStack;
using ServiceStack.DataAnnotations;

namespace OnlineShoppinAPI.Models
{
    [Alias("CUS01")]
    public class CUS01
    {
        /// <summary>
        /// Customer Id
        /// </summary>
        [AutoIncrement]
        public int S01F01 { get; set; }

        /// <summary>
        /// Customer Name
        /// </summary>
        [Required]
        [StringLength(maximumLength: 50)]
        public string S01F02 { get; set; }

        [Required]
        [ValidateEmail]
        public string S01F03 { get; set; }

        [Required]
        [StringLength(minimumLength: 10, maximumLength: 10)]
        public string S01F04 { get; set; }

        [Required]
        [StringLength(minimumLength:8, maximumLength: 16)]
        public string S01F06 { get; set; }
    }
}