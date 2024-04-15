namespace OnlineShoppingAPI.Models.POCO
{
    /// <summary>
    /// SUP01 model for stopring the supplier's details.
    /// </summary>
    public class SUP01
    {
        /// <summary>
        /// Suplier Id
        /// </summary>
        public int P01F01 { get; set; }

        /// <summary>
        /// Suplier name
        /// </summary>
        public string P01F02 { get; set; }

        /// <summary>
        /// Suplier Email Id
        /// </summary>
        public string P01F03 { get; set; }

        /// <summary>
        /// Suplier password
        /// </summary>
        public string P01F04 { get; set; }

        /// <summary>
        /// Suplier Mobile Number
        /// </summary>
        public string P01F05 { get; set; }

        /// <summary>
        /// Suplier GST Number
        /// </summary>
        public string P01F06 { get; set; }
    }
}