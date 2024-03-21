namespace FilterDemo.Model
{
    /// <summary>
    /// User POCO model for authentication purpose.
    /// </summary>
    public class USR01
    {
        /// <summary>
        /// User Id
        /// </summary>
        public int R01F01 { get; set; }

        /// <summary>
        /// User's Username
        /// </summary>
        public string R01F02 { get; set; }

        /// <summary>
        /// User's Password
        /// </summary>
        public string R01F03 { get; set; }

        /// <summary>
        /// User's Role
        /// </summary>
        public string R01F04 { get; set; }
    }
}
