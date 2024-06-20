namespace DataGridFinalDemoAPI.Models
{
    /// <summary>
    /// Student Model for storing the student information.
    /// </summary>
    public class STU01
    {
        /// <summary>
        /// Student Id
        /// </summary>
        public int U01F01 { get; set; }

        /// <summary>
        /// Student Name
        /// </summary>
        public string? U01F02 { get; set; }

        /// <summary>
        /// Student Age
        /// </summary>
        public int U01F03 { get; set; }

        /// <summary>
        /// Student Email Address
        /// </summary>
        public string? U01F04 { get; set; }

        /// <summary>
        /// Student Mobile Number
        /// </summary>
        public string? U01F05 { get; set; }

        /// <summary>
        /// City Id (Foreign Key)
        /// </summary>
        public int U01F06 { get; set; }
    }
}
