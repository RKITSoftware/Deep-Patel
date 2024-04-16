using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PlacementCellManagementAPI.Models.Dtos
{
    /// <summary>
    /// Data Transfer Object (DTO) for User Login.
    /// </summary>
    public class DTOUSR01
    {
        /// <summary>
        /// Gets or sets the Username.
        /// </summary>
        [Required(ErrorMessage = "Username is required.")]
        [JsonProperty("R01102")]
        public string? R01F02 { get; set; }

        /// <summary>
        /// Gets or sets the Password.
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        [JsonProperty("R01104")]
        public string? R01F04 { get; set; }
    }
}
