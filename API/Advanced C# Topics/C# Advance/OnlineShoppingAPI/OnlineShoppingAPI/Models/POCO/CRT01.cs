using Newtonsoft.Json;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace OnlineShoppingAPI.Models.POCO
{
    [Alias("CRT01")]
    public class CRT01
    {
        /// <summary>
        /// Cart id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int T01F01 { get; set; }

        /// <summary>
        /// Customer id foreign key for cart
        /// </summary>
        [References(typeof(CUS01))]
        [JsonProperty("T01102")]
        public int T01F02 { get; set; }

        /// <summary>
        /// Product id foreign key for cart
        /// </summary>
        [References(typeof(PRO02))]
        [JsonProperty("T01103")]
        public int T01F03 { get; set; }

        /// <summary>
        /// Product Quantity that customer need
        /// </summary>
        [ValidateNotNull]
        [JsonProperty("T01104")]
        public int T01F04 { get; set; }

        /// <summary>
        /// Product Prize for specific Quantity
        /// </summary>
        public int T01F05 { get; set; }
    }
}