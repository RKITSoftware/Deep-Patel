using ServiceStack.DataAnnotations;
using System;

namespace VerificationDemo.Models.POCO
{
    /// <summary>
    /// USR01 POCO model conatainign the information about the user.
    /// </summary>
    public class USR01
    {
        /// <summary>
        /// User id
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int R01F01 { get; set; }

        /// <summary>
        ///  User's Name
        /// </summary>
        [Required]
        [Unique]
        public string R01F02 { get; set; }

        /// <summary>
        /// User's Age
        /// </summary>
        [Range(1, 99)]
        public int R01F03 { get; set; }

        /// <summary>
        /// Created At Time
        /// </summary>
        public DateTime R01F04 { get; set; }

        /// <summary>
        /// Updated at time
        /// </summary>
        public DateTime R01F05 { get; set; }
    }
}