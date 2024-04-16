using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PlacementCellManagementAPI.Models.Dtos
{
    /// <summary>
    /// Data transfer object for Company.
    /// </summary>
    public class DTOCMP01
    {
        /// <summary>
        /// Gets or sets the Company Name.
        /// </summary>
        [Required(ErrorMessage = "Company name is required.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Company name must be between 5 to 50 characters.")]
        [JsonProperty("P01102")]
        public string? P01F02 { get; set; }

        /// <summary>
        /// Gets or sets the Company Location.
        /// </summary>
        [Required(ErrorMessage = "Company location is required.")]
        [JsonProperty("P01103")]
        public string? P01F03 { get; set; }
    }
}
