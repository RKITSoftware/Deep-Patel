using System.Text.Json.Serialization;

namespace OnlineShoppingAPI.Models.DTO
{
    /// <summary>
    /// DTO for PRO02 Model.
    /// </summary>
    public class DTOPRO02
    {
        /// <summary>
        /// Product Id
        /// </summary>
        [JsonPropertyName("O02F01")]
        public int O02101 { get; set; }

        /// <summary>
        /// Product Name
        /// </summary>
        [JsonPropertyName("O02F02")]
        public string O02102 { get; set; }

        /// <summary>
        /// Product Buy Price
        /// </summary>
        [JsonPropertyName("O02F03")]
        public int O02103 { get; set; }

        /// <summary>
        /// Product Sell Price
        /// </summary>
        [JsonPropertyName("O02F04")]
        public int O02104 { get; set; }

        /// <summary>
        /// Product Quantity
        /// </summary>
        [JsonPropertyName("O02F05")]
        public decimal O02105 { get; set; }

        /// <summary>
        /// Product Image
        /// </summary>
        [JsonPropertyName("O02F06")]
        public string O02106 { get; set; }

        /// <summary>
        /// Foreign Key of Category Table
        /// </summary>
        [JsonPropertyName("O02F09")]
        public int O02109 { get; set; }

        /// <summary>
        /// Foreign Key of Suplier Table
        /// </summary>
        [JsonPropertyName("O02F10")]
        public int O01110 { get; set; }
    }
}