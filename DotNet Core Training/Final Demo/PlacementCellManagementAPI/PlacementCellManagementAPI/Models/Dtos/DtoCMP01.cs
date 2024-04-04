using System.ComponentModel.DataAnnotations;

namespace PlacementCellManagementAPI.Models.Dtos
{
    /// <summary>
    /// Data transfer object for Company
    /// </summary>
    public class DtoCMP01
    {
        /// <summary>
        /// Company Name
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Company name must be between 5 to 50 characters.")]
        public string P01101 { get; set; }

        /// <summary>
        /// Company Location
        /// </summary>
        [Required]
        public string P01102 { get; set; }
    }
}
