using Newtonsoft.Json;
using ServiceStack.DataAnnotations;

namespace PlacementCellManagementAPI.Models.Dtos
{
    /// <summary>
    /// DTO for User Login
    /// </summary>
    public class DtoUSR01
    {
        /// <summary>
        /// Username
        /// </summary>
        [Required]
        [JsonProperty("R01F02")]
        public string R01102 { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [JsonProperty("R01F04")]
        public string R01104 { get; set; }
    }
}
