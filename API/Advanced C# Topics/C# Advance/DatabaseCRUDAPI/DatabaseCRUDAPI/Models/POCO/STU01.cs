using System;

namespace DatabaseCRUDAPI.Models.POCO
{
    /// <summary>
    /// Student table stores information of student
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
        public string U01F02 { get; set; }

        /// <summary>
        /// Student Age
        /// </summary>
        public int U01F03 { get; set; }

        /// <summary>
        /// Created At
        /// </summary>
        public DateTime U01F04 { get; set; }

        /// <summary>
        /// Updated At
        /// </summary>
        public DateTime U01F05 { get; set; }
    }
}