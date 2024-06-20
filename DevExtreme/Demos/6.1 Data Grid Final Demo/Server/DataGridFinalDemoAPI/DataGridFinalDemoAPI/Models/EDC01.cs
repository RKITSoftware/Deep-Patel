namespace DataGridFinalDemoAPI.Models
{
    /// <summary>
    /// Education model stores the education details of the students
    /// </summary>
    public class EDC01
    {
        /// <summary>
        /// Primary Key (Education Id)
        /// </summary>
        public int C01F01 { get; set; }

        /// <summary>
        /// Name of the school.
        /// </summary>
        public string? C01F02 { get; set; }

        /// <summary>
        /// Standard
        /// </summary>
        public string? C01F03 { get; set; }

        /// <summary>
        /// Percentage
        /// </summary>
        public double C01F04 { get; set; }

        /// <summary>
        /// Student Id
        /// </summary>
        public int C01F05 { get; set; }
    }
}
