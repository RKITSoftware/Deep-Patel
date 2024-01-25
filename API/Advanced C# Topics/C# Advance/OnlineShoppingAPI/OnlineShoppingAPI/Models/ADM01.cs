using ServiceStack;
using ServiceStack.DataAnnotations;

namespace OnlineShoppingAPI.Models
{
    public class ADM01
    {
        /// <summary>
        /// Admin Id
        /// </summary>
        [AutoIncrement]
        public int M01F01 { get; set; }

        /// <summary>
        /// Admin Name
        /// </summary>
        [Required]
        [StringLength(50)]
        public string M01F02 { get; set; }

        /// <summary>
        /// Admin Email Address
        /// </summary>
        [Required]
        [ValidateEmail]
        public string M01F03 { get; set; }

        /// <summary>
        /// Admin Password
        /// </summary>
        [Required]
        [StringLength(minimumLength: 8, maximumLength: 16)]
        public string M01F04 { get; set; }
    }
}