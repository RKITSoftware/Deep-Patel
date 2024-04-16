using ServiceStack.DataAnnotations;

namespace OnlineShoppingAPI.Models.POCO
{
    /// <summary>
    /// Category POCO Model
    /// </summary>
    public class CAT01
    {
        /// <summary>
        /// Category Id
        /// </summary>
        [PrimaryKey]
        public int T01F01 { get; set; }

        /// <summary>
        /// Category Type
        /// </summary>
        public string T01F02 { get; set; }
    }
}