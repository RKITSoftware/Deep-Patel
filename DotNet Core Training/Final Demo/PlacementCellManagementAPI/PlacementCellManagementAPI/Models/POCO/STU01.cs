using ServiceStack.DataAnnotations;

namespace PlacementCellManagementAPI.Models.POCO
{
    /// <summary>
    /// Student POCO Model for storing the information of a student.
    /// </summary>
    public class STU01
    {
        /// <summary>
        /// Gets or sets the Student Id.
        /// </summary>
        [PrimaryKey]
        public int U01F01 { get; set; }

        /// <summary>
        /// Gets or sets the Student's First Name.
        /// </summary>
        public string? U01F02 { get; set; }

        /// <summary>
        /// Gets or sets the Student's Last Name.
        /// </summary>
        public string? U01F03 { get; set; }

        /// <summary>
        /// Gets or sets the Student's Date of Birth.
        /// </summary>
        public DateTime U01F04 { get; set; }

        /// <summary>
        /// Gets or sets the Student's Gender.
        /// </summary>
        public string? U01F05 { get; set; }

        /// <summary>
        /// Gets or sets the Student's Aadhar Card Number.
        /// </summary>
        public string? U01F06 { get; set; }

        /// <summary>
        /// Gets or sets the Foreign Key of the associated User.
        /// </summary>
        public int U01F07 { get; set; }
    }
}
