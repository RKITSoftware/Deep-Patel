using ServiceStack.DataAnnotations;

namespace PlacementCellManagementAPI.Models.POCO
{
    /// <summary>
    /// User POCO model for storing the user security related information.
    /// </summary>
    public class USR01
    {
        /// <summary>
        /// Gets or sets the User Id.
        /// </summary>
        [PrimaryKey]
        public int R01F01 { get; set; }

        /// <summary>
        /// Gets or sets the Username.
        /// </summary>
        public string? R01F02 { get; set; }

        /// <summary>
        /// Gets or sets the User's Email.
        /// </summary>
        public string? R01F03 { get; set; }

        /// <summary>
        /// Gets or sets the User's Secured Password.
        /// </summary>
        public string? R01F04 { get; set; }

        /// <summary>
        /// Gets or sets the User's Roles.
        /// </summary>
        public string? R01F05 { get; set; }
    }
}
