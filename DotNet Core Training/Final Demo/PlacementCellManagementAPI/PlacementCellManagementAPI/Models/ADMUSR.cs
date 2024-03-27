namespace PlacementCellManagementAPI.Models
{
    /// <summary>
    /// Admin user relational model
    /// </summary>
    public class ADMUSR
    {
        /// <summary>
        /// Admin
        /// </summary>
        public ADM01 M01 { get; set; }

        /// <summary>
        /// User
        /// </summary>
        public USR01 R01 { get; set; }
    }
}
