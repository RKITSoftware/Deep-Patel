using ServiceStack.DataAnnotations;
using System.Text.Json.Serialization;

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
        [JsonPropertyName("T01101")]
        public int T01F01 { get; set; }

        /// <summary>
        /// Category Type
        /// </summary>
        [Unique]
        [JsonPropertyName("T01102")]
        public string T01F02 { get; set; }
    }
}