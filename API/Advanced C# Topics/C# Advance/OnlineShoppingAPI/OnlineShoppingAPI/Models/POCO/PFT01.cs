using ServiceStack.DataAnnotations;

namespace OnlineShoppingAPI.Models.POCO
{
    /// <summary>
    /// Profit table contains the details about specific date's profit
    /// </summary>
    public class PFT01
    {
        /// <summary>
        /// Id
        /// </summary>
        [PrimaryKey]
        public int T01F01 { get; set; }

        /// <summary>
        /// Date
        /// </summary>
        public string T01F02 { get; set; }

        /// <summary>
        /// Profit specific to that date
        /// </summary>
        public decimal T01F03 { get; set; } = 0;
    }
}