using ServiceStack.DataAnnotations;

namespace OnlineShoppingAPI.Models
{
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
        public string T01F02 { get; set; }
    }
}