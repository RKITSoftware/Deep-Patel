namespace BookMyShowAPI.Model
{
    /// <summary>
    /// Shows table to store infomration about shows to specific theatres.
    /// </summary>
    public class SHW01
    {
        /// <summary>
        /// Show id
        /// </summary>
        public int W01F01 { get; set; }

        /// <summary>
        /// Movie id
        /// </summary>
        public int W01F02 { get; set; }

        /// <summary>
        /// Theatre id
        /// </summary>
        public int W01F03 { get; set; }

        /// <summary>
        /// Show time
        /// </summary>
        public DateTime W01F04 { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        public decimal W01F05 { get; set; }
    }
}
