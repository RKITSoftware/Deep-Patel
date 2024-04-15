namespace OnlineShoppingAPI.Models.POCO
{
    /// <summary>
    /// PRO01 model to store the product information.
    /// </summary>
    public class PRO01
    {
        /// <summary>
        /// Product Id
        /// </summary>
        public int O01F01 { get; set; }

        /// <summary>
        /// Product Name
        /// </summary>
        public string O01F02 { get; set; }

        /// <summary>
        /// Product Price
        /// </summary>
        public int O01F03 { get; set; }

        /// <summary>
        /// Product Quantity
        /// </summary>
        public decimal O01F04 { get; set; }

        /// <summary>
        /// Product Image
        /// </summary>
        public string O01F05 { get; set; }

        /// <summary>
        /// Foreign Key of Suplier Table
        /// </summary>
        public int O01F06 { get; set; }
    }
}