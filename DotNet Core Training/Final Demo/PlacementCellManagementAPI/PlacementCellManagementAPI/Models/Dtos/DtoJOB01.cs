namespace PlacementCellManagementAPI.Models.Dtos
{
    /// <summary>
    /// Job Dto for handling data transfer for job related information.
    /// </summary>
    public class DtoJOB01
    {
        /// <summary>
        /// Job Title
        /// </summary>
        public string B01101 { get; set; }

        /// <summary>
        /// Job Capacity
        /// </summary>
        public int B01102 { get; set; }

        /// <summary>
        /// Salary Starting Range
        /// </summary>
        public int B01103 { get; set; }

        /// <summary>
        /// Salary Ending Range
        /// </summary>
        public int B01104 { get; set; }

        /// <summary>
        /// Last date for form fill
        /// </summary>
        public DateTime B01105 { get; set; }

        /// <summary>
        /// Form Link
        /// </summary>
        public string B01106 { get; set; }

        /// <summary>
        /// Company Id
        /// </summary>
        public int B01107 { get; set; }
    }
}
