using Newtonsoft.Json;

namespace ConverterDemo.Dto
{
    public class DTOUSR01
    {
        /// <summary>
        /// First name
        /// </summary>
        [JsonProperty("R01F02")]
        public string R01102 { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        [JsonProperty("R01F03")]
        public string R01103 { get; set; }

        /// <summary>
        /// Date of Birth
        /// </summary>
        [JsonProperty("R01F04")]
        public DateTime R01104 { get; set; }
    }
}
