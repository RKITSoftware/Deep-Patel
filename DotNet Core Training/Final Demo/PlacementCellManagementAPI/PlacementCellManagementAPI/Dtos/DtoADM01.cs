namespace PlacementCellManagementAPI.Dtos
{
    /// <summary>
    /// Data Transfer Object (DTO) for admin.
    /// </summary>
    public class DtoADM01
    {
        /// <summary>
        /// Admin Full Name
        /// </summary>
        public string M01101 { get; set; }

        /// <summary>
        /// Date of Birth
        /// </summary>
        public DateTime M01102 { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        public string M01103 { get; set; }

        /// <summary>
        /// Admin's Username
        /// </summary>
        public string M01104 { get; set; }

        /// <summary>
        /// Admin's Email
        /// </summary>
        public string M01105 { get; set; }

        /// <summary>
        /// Admin's Password
        /// </summary>
        public string M01106 { get; set; }
    }
}
