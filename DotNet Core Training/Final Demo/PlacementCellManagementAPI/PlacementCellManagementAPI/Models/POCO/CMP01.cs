using ServiceStack.DataAnnotations;

namespace PlacementCellManagementAPI.Models.POCO
{
    /// <summary>
    /// Company POCO model for handling database related queries.
    /// </summary>
    public class CMP01
    {
        /// <summary>
        /// Gets or sets the Company Id.
        /// </summary>
        [PrimaryKey]
        public int P01F01 { get; set; }

        /// <summary>
        /// Gets or sets the Company Name.
        /// </summary>
        public string? P01F02 { get; set; }

        /// <summary>
        /// Gets or sets the Company Location.
        /// </summary>
        public string? P01F03 { get; set; }
    }
}
