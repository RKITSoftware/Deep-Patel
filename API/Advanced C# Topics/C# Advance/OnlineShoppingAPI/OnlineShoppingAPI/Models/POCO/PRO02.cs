using Newtonsoft.Json;
using ServiceStack.DataAnnotations;
using System;

namespace OnlineShoppingAPI.Models.POCO
{
    [Alias("PRO02")]
    public class PRO02
    {
        /// <summary>
        /// Product Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        [JsonProperty("O02101")]
        public int O02F01 { get; set; }

        /// <summary>
        /// Product Name
        /// </summary>
        [Required]
        [StringLength(100)]
        [JsonProperty("O02102")]
        public string O02F02 { get; set; }

        /// <summary>
        /// Product Buy Price
        /// </summary>
        [Required]
        [DecimalLength(10, 2)]
        [JsonProperty("O02103")]
        public int O02F03 { get; set; }

        /// <summary>
        /// Product Sell Price
        /// </summary>
        [Required]
        [DecimalLength(10, 2)]
        [JsonProperty("O02104")]
        public int O02F04 { get; set; }

        /// <summary>
        /// Product Quantity
        /// </summary>
        [Required]
        [JsonProperty("O02105")]
        public decimal O02F05 { get; set; }

        /// <summary>
        /// Product Image
        /// </summary>
        [JsonProperty("O02106")]
        public string O02F06 { get; set; }

        /// <summary>
        /// Product Status
        /// </summary>
        [Required]
        public int O02F07 { get; set; }

        /// <summary>
        /// Product Creation Time
        /// </summary>
        [Required]
        public DateTime O02F08 { get; set; }

        /// <summary>
        /// Foreign Key of Category Table
        /// </summary>
        [Required]
        [References(typeof(CAT01))]
        [JsonProperty("O02109")]
        public int O02F09 { get; set; }

        /// <summary>
        /// Foreign Key of Suplier Table
        /// </summary>
        [Required]
        [References(typeof(SUP01))]
        [JsonProperty("O02110")]
        public int O02F10 { get; set; }
    }
}