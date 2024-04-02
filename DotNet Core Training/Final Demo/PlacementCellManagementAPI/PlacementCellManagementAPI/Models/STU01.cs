using Newtonsoft.Json;

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
        public int? U01F01 { get; set; }

        /// <summary>
        /// Student's First Name
        /// </summary>
        [JsonProperty("U01102")]
        public string U01F02 { get; set; }

        /// <summary>
        /// Student's Last Name
        /// </summary>
        [JsonProperty("U01103")]
        public string U01F03 { get; set; }

        /// <summary>
        /// Student's Date of Birth
        /// </summary>
        [JsonProperty("U01104")]
        public DateTime U01F04 { get; set; }

        /// <summary>
        /// Student's Gender
        /// </summary>
        [JsonProperty("U01105")]
        public string U01F05 { get; set; }

        /// <summary>
        /// Student's Aadhar Card Number
        /// </summary>
        [JsonProperty("U01106")]
        public string U01F06 { get; set; }

        /// <summary>
        /// Foreign Key of User
        /// </summary>
        [JsonProperty("U01107")]
        public int U01F07 { get; set; }
    }
}
