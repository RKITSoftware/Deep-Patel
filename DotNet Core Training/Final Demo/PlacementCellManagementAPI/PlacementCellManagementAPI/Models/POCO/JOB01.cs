using Newtonsoft.Json;

namespace PlacementCellManagementAPI.Models.POCO
{
    public class JOB01
    {
        /// <summary>
        /// Job Id
        /// </summary>
        public int? B01F01 { get; set; }

        /// <summary>
        /// Job Title
        /// </summary>
        [JsonProperty("B01102")]
        public string B01F02 { get; set; }

        /// <summary>
        ///  Job Capacity
        /// </summary>
        [JsonProperty("B01103")]
        public int B01F03 { get; set; }

        /// <summary>
        /// Salary Starting Range
        /// </summary>
        [JsonProperty("B01104")]
        public int B01F04 { get; set; }

        /// <summary>
        /// Salary Ending Range
        /// </summary>
        [JsonProperty("B01105")]
        public int B01F05 { get; set; }

        /// <summary>
        /// Company Id
        /// </summary>
        [JsonProperty("B01106")]
        public int B01F06 { get; set; }

        /// <summary>
        /// Form Fill Last Date
        /// </summary>
        [JsonProperty("B01107")]
        public DateTime B01F07 { get; set; }

        /// <summary>
        /// Link
        /// </summary>
        [JsonProperty("B01108")]
        public string B01F08 { get; set; }
    }
}
