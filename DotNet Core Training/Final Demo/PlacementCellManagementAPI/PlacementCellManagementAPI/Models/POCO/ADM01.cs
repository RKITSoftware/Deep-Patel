using Newtonsoft.Json;

namespace PlacementCellManagementAPI.Models.POCO
{
    /// <summary>
    /// Admin POCO model to store the admin's information.
    /// </summary>
    public class ADM01
    {
        /// <summary>
        /// Admin Id
        /// </summary>
        public int M01F01 { get; set; }

        /// <summary>
        /// Admin First name
        /// </summary>
        [JsonProperty("M01102")]
        public string M01F02 { get; set; }

        /// <summary>
        /// Admin Last name
        /// </summary>
        [JsonProperty("M01103")]
        public string M01F03 { get; set; }

        /// <summary>
        /// Admin's Date of Birth
        /// </summary>
        [JsonProperty("M01104")]
        public DateTime M01F04 { get; set; }

        /// <summary>
        /// Admin's Gender
        /// </summary>
        [JsonProperty("M01105")]
        public string M01F05 { get; set; }

        /// <summary>
        /// Foreign Key of User
        /// </summary>
        [JsonProperty("M01106")]
        public int M01F06 { get; set; }
    }
}
