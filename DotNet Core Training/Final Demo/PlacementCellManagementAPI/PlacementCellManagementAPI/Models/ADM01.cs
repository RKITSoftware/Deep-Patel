namespace PlacementCellManagementAPI.Models
{
    /// <summary>
    /// Admin POCO model to store the admin's information.
    /// </summary>
    public class ADM01
    {
        /// <summary>
        /// Admin Id
        /// </summary>
        public int M01F01 { get; set; }

        /// <summary>
        /// Admin First name
        /// </summary>
        public string M01F02 { get; set; }

        /// <summary>
        /// Admin Last name
        /// </summary>
        public string M01F03 { get; set; }

        /// <summary>
        /// Admin's Date of Birth
        /// </summary>
        public DateTime M01F04 { get; set; }

        /// <summary>
        /// Admin's Gender
        /// </summary>
        public string M01F05 { get; set; }

        /// <summary>
        /// Foreign Key of User
        /// </summary>
        public int M01F06 { get; set; }
    }
}
