using Newtonsoft.Json;

namespace OnlineShoppingAPI.Models.DTO
{
    public class DTOPRO02
    {
        /// <summary>
        /// Product Id
        /// </summary>
        [JsonProperty("O02F01")]
        public int O02101 { get; set; }

        /// <summary>
        /// Product Name
        /// </summary>
        [JsonProperty("O02F02")]
        public string O02102 { get; set; }

        /// <summary>
        /// Product Buy Price
        /// </summary>
        [JsonProperty("O02F03")]
        public int O02103 { get; set; }

        /// <summary>
        /// Product Sell Price
        /// </summary>
        [JsonProperty("O02F04")]
        public int O02104 { get; set; }

        /// <summary>
        /// Product Quantity
        /// </summary>
        [JsonProperty("O02F05")]
        public decimal O02105 { get; set; }

        /// <summary>
        /// Product Image
        /// </summary>
        [JsonProperty("O02F06")]
        public string O02106 { get; set; }

        /// <summary>
        /// Foreign Key of Category Table
        /// </summary>
        [JsonProperty("O02F09")]
        public int O02109 { get; set; }

        /// <summary>
        /// Foreign Key of Suplier Table
        /// </summary>
        [JsonProperty("O02F10")]
        public int O01110 { get; set; }
    }
}