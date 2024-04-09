using Newtonsoft.Json;

namespace OnlineShoppingAPI.Models.DTO
{
    public class DTOCRT01
    {
        /// <summary>
        /// Customer id foreign key for cart
        /// </summary>
        [JsonProperty("T01F02")]
        public int T01102 { get; set; }

        /// <summary>
        /// Product id foreign key for cart
        /// </summary>
        [JsonProperty("T01F03")]
        public int T01103 { get; set; }

        /// <summary>
        /// Product Quantity that customer need
        /// </summary>
        [JsonProperty("T01F04")]
        public int T01104 { get; set; }
    }
}