using Newtonsoft.Json;
using ServiceStack.DataAnnotations;

namespace OnlineShoppingAPI.Models.POCO
{
    /// <summary>
    /// Category POCO Model
    /// </summary>
    [Alias("CAT01")]
    public class CAT01
    {
        /// <summary>
        /// Category Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int T01F01 { get; set; }

        /// <summary>
        /// Category Type
        /// </summary>
        [Unique]
        [JsonProperty("T01102")]
        public string T01F02 { get; set; }
    }
}