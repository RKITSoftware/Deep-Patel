using Newtonsoft.Json;

namespace OnlineShoppingAPI.Models.DTO
{
    /// <summary>
    /// DTO for PRO01 Model.
    /// </summary>
    public class DTOPRO01
    {
        /// <summary>
        /// Product Name
        /// </summary>
        [JsonProperty("O01F02")]
        public string O01102 { get; set; }

        /// <summary>
        /// Product Price
        /// </summary>
        [JsonProperty("O01103")]
        public int O01103 { get; set; }

        /// <summary>
        /// Product Quantity
        /// </summary>
        [JsonProperty("O01104")]
        public decimal O01104 { get; set; }

        /// <summary>
        /// Product Image
        /// </summary>
        [JsonProperty("O01105")]
        public string O01105 { get; set; }

        /// <summary>
        /// Foreign Key of Suplier Table
        /// </summary>
        [JsonProperty("O01106")]
        public int O01F06 { get; set; }
    }
}