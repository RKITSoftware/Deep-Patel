namespace PlacementCellManagementAPI.Models
{
    /// <summary>
    /// Student User relational model
    /// </summary>
    public class STUUSR
    {
        /// <summary>
        /// Student Model
        /// </summary>
        public STU01 U01 { get; set; }

        /// <summary>
        /// User Model
        /// </summary>
        public USR01 R01 { get; set; }
    }
}
