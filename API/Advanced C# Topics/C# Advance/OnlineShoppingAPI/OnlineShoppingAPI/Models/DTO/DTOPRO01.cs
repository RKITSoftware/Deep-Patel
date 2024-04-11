using System.Text.Json.Serialization;

namespace OnlineShoppingAPI.Models.DTO
{
    /// <summary>
    /// DTO for PRO01 Model.
    /// </summary>
    public class DTOPRO01
    {
        /// <summary>
        /// Product Id
        /// </summary>
        [JsonPropertyName("O01F01")]
        public int O01101 { get; set; }

        /// <summary>
        /// Product Name
        /// </summary>
        [JsonPropertyName("O01F02")]
        public string O01102 { get; set; }

        /// <summary>
        /// Product Price
        /// </summary>
        [JsonPropertyName("O01F03")]
        public int O01103 { get; set; }

        /// <summary>
        /// Product Quantity
        /// </summary>
        [JsonPropertyName("O01F04")]
        public decimal O01104 { get; set; }

        /// <summary>
        /// Product Image
        /// </summary>
        [JsonPropertyName("O01F05")]
        public string O01105 { get; set; }

        /// <summary>
        /// Foreign Key of Suplier Table
        /// </summary>
        [JsonPropertyName("O01F06")]
        public int O01106 { get; set; }
    }
}