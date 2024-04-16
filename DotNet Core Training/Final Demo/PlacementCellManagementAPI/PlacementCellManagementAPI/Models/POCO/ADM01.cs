using ServiceStack.DataAnnotations;

namespace PlacementCellManagementAPI.Models.POCO
{
    /// <summary>
    /// Admin POCO model to store the admin's information.
    /// </summary>
    public class ADM01
    {
        /// <summary>
        /// Gets or sets the Admin Id.
        /// </summary>
        [PrimaryKey]
        public int M01F01 { get; set; }

        /// <summary>
        /// Gets or sets the Admin's First Name.
        /// </summary>
        public string? M01F02 { get; set; }

        /// <summary>
        /// Gets or sets the Admin's Last Name.
        /// </summary>
        public string? M01F03 { get; set; }

        /// <summary>
        /// Gets or sets the Admin's Date of Birth.
        /// </summary>
        public DateTime M01F04 { get; set; }

        /// <summary>
        /// Gets or sets the Admin's Gender.
        /// </summary>
        public string? M01F05 { get; set; }

        /// <summary>
        /// Gets or sets the Foreign Key of the associated User.
        /// </summary>
        public int M01F06 { get; set; }
    }
}
