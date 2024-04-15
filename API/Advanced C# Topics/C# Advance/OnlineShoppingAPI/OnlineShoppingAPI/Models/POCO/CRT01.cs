namespace OnlineShoppingAPI.Models.POCO
{
    /// <summary>
    /// CRT01 model to store the cart information of customer.
    /// </summary>
    public class CRT01
    {
        /// <summary>
        /// Cart id
        /// </summary>
        public int T01F01 { get; set; }

        /// <summary>
        /// Customer id foreign key for cart
        /// </summary>
        public int T01F02 { get; set; }

        /// <summary>
        /// Product id foreign key for cart
        /// </summary>
        public int T01F03 { get; set; }

        /// <summary>
        /// Product Quantity that customer need
        /// </summary>
        public int T01F04 { get; set; }

        /// <summary>
        /// Product Prize for specific Quantity
        /// </summary>
        public int T01F05 { get; set; }
    }
}