using System;

namespace SchoolManagementAPI.Models
{
    /// <summary>
    /// Students model stores the information about a students.
    /// </summary>
    public class STU01
    {
        /// <summary>
        /// Student's ID
        /// </summary>
        public int U01F01 { get; set; }

        /// <summary>
        /// Student's Name
        /// </summary>
        public string U01F02 { get; set; }

        /// <summary>
        /// Student's Email Id
        /// </summary>
        public string U01F03 { get; set; }

        /// <summary>
        /// Student's Password
        /// </summary>
        public string U01F04 { get; set; }

        /// <summary>
        /// Student's Date of Birth
        /// </summary>
        public DateTime U01F05 { get; set; }

        /// <summary>
        /// Student's Gender
        /// </summary>
        public bool U01F06 { get; set; }

        /// <summary>
        /// Student's Mobile Number
        /// </summary>
        public string U01F07 { get; set; }
    }
}