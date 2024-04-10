﻿using Newtonsoft.Json;
using ServiceStack;
using ServiceStack.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlineShoppingAPI.Models.POCO
{
    public class ADM01
    {
        /// <summary>
        /// Admin Id
        /// </summary>
        [AutoIncrement]
        public int M01F01 { get; set; }

        /// <summary>
        /// Admin Name
        /// </summary>
        [Required]
        [StringLength(50)]
        [JsonPropertyName("M01102")]
        public string M01F02 { get; set; }

        /// <summary>
        /// Admin Email Address
        /// </summary>
        [Required]
        [ValidateEmail]
        [JsonPropertyName("M01103")]
        public string M01F03 { get; set; }
    }
}