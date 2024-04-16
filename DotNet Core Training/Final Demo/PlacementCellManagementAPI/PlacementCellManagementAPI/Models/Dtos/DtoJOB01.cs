using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PlacementCellManagementAPI.Models.Dtos
{
    /// <summary>
    /// Job DTO for handling data transfer for job related information.
    /// </summary>
    public class DTOJOB01
    {
        /// <summary>
        /// Gets or sets the Job Title.
        /// </summary>
        [Required(ErrorMessage = "Job title is required.")]
        [JsonProperty("B01102")]
        public string? B01F02 { get; set; }

        /// <summary>
        /// Gets or sets the Job Capacity.
        /// </summary>
        [Required(ErrorMessage = "Job capacity is required.")]
        [JsonProperty("B01103")]
        public int B01F03 { get; set; }

        /// <summary>
        /// Gets or sets the Salary Starting Range.
        /// </summary>
        [Required(ErrorMessage = "Starting salary range is required.")]
        [JsonProperty("B01104")]
        public int B01F04 { get; set; }

        /// <summary>
        /// Gets or sets the Salary Ending Range.
        /// </summary>
        [Required(ErrorMessage = "Ending salary range is required.")]
        [JsonProperty("B01105")]
        public int B01F05 { get; set; }

        /// <summary>
        /// Gets or sets the Company Id.
        /// </summary>
        [Required(ErrorMessage = "Company Id is required.")]
        [JsonProperty("B01106")]
        public int B01F06 { get; set; }

        /// <summary>
        /// Gets or sets the Last date for form fill.
        /// </summary>
        [JsonProperty("B01107")]
        public DateTime? B01F07 { get; set; }

        /// <summary>
        /// Gets or sets the Form Link.
        /// </summary>
        [JsonProperty("B01108")]
        public string? B01F08 { get; set; }
    }
}
