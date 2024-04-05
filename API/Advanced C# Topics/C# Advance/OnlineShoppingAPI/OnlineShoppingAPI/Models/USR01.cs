using Newtonsoft.Json;
using ServiceStack.DataAnnotations;

namespace OnlineShoppingAPI.Models
{
    [Alias("USR01")]
    public class USR01
    {
        /// <summary>
        /// User Id
        /// </summary>
        [AutoIncrement]
        public int R01F01 { get; set; }

        /// <summary>
        /// User username
        /// </summary>
        public string R01F02 { get; set; }

        /// <summary>
        /// User Password
        /// </summary>
        [Required]
        [StringLength(minimumLength: 8, maximumLength: 16)]
        [JsonProperty("R01103")]
        public string R01F03 { get; set; }

        /// <summary>
        /// User Role
        /// </summary>
        public string R01F04 { get; set; }

        /// <summary>
        /// Encrypt Password
        /// </summary>
        public string R01F05 { get; set; }
    }
}