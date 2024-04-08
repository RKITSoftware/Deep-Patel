using ServiceStack.DataAnnotations;

namespace OnlineShoppingAPI.Models.POCO
{
    /// <summary>
    /// Profit table contains the details about specific date's profit
    /// </summary>
    [Alias("PFT01")]
    public class PFT01
    {
        /// <summary>
        /// Id
        /// </summary>
        [AutoIncrement]
        public int T01F01 { get; set; }

        /// <summary>
        /// Date
        /// </summary>
        [Required]
        public string T01F02 { get; set; }

        /// <summary>
        /// Profit specific to that date
        /// </summary>
        [Default(0)]
        public decimal T01F03 { get; set; } = 0;
    }
}