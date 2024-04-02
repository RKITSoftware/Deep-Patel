using Newtonsoft.Json;

namespace ConverterDemo.Model
{
    public class USR01
    {
        /// <summary>
        /// User ID
        /// </summary>
        public int? R01F01 { get; set; }

        /// <summary>
        /// User's first name
        /// </summary>
        [JsonProperty("R01102")]
        public string R01F02 { get; set; }

        /// <summary>
        /// LAst name
        /// </summary>
        [JsonProperty("R01103")]
        public string R01F03 { get; set; }

        /// <summary>
        /// Date of Birth
        /// </summary>
        [JsonProperty("R01104")]
        public DateTime R01F04 { get; set; }
    }
}
