namespace PlacementCellManagementAPI.Models
{
    /// <summary>
    /// Student POCO Model for storing the information of student.
    /// </summary>
    public class STU01
    {
        /// <summary>
        /// Student Id
        /// </summary>
        public int U01F01 { get; set; }

        /// <summary>
        /// Student's First Name
        /// </summary>
        public string U01F02 { get; set; }

        /// <summary>
        /// Student's Last Name
        /// </summary>
        public string U01F03 { get; set; }

        /// <summary>
        /// Student's Date of Birth
        /// </summary>
        public DateTime U01F04 { get; set; }

        /// <summary>
        /// Student's Gender
        /// </summary>
        public string U01F05 { get; set; }

        /// <summary>
        /// Student's Aadhar Card Number
        /// </summary>
        public string U01F06 { get; set; }

        /// <summary>
        /// Foreign Key of User
        /// </summary>
        public int U01F07 { get; set; }
    }
}
