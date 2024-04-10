﻿using System.Text.Json.Serialization;

namespace OnlineShoppingAPI.Models.DTO
{
    /// <summary>
    /// DTO for CUS01 Model
    /// </summary>
    public class DTOCUS01
    {
        /// <summary>
        /// Customer Id
        /// </summary>
        [JsonPropertyName("S01F01")]
        public int S01101 { get; set; }

        /// <summary>
        /// Customer Name
        /// </summary>
        [JsonPropertyName("S01F02")]
        public string S01102 { get; set; }

        /// <summary>
        /// Customer Email Address
        /// </summary>
        [JsonPropertyName("S01F03")]
        public string S01103 { get; set; }

        /// <summary>
        /// Customer Password
        /// </summary>
        [JsonPropertyName("S01F04")]
        public string S01104 { get; set; }

        /// <summary>
        /// Customer Mobile Number
        /// </summary>
        [JsonPropertyName("S01F05")]
        public string S01105 { get; set; }

        /// <summary>
        /// Customer Address
        /// </summary>
        [JsonPropertyName("S01F06")]
        public string S01106 { get; set; }
    }
}