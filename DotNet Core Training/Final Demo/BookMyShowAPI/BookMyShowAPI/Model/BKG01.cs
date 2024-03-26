namespace BookMyShowAPI.Model
{
    /// <summary>
    /// Booking model to store the booking related informations.
    /// </summary>
    public class BKG01
    {
        /// <summary>
        /// Booking id
        /// </summary>
        public int G01F01 { get; set; }

        /// <summary>
        /// User id
        /// </summary>
        public int G01F02 { get; set; }

        /// <summary>
        /// Show id
        /// </summary>
        public int G01F03 { get; set; }

        /// <summary>
        /// Number of Tickets
        /// </summary>
        public int G01F04 { get; set; }

        /// <summary>
        /// Booking date and time
        /// </summary>
        public DateTime G01F05 { get; set; }

        /// <summary>
        /// Total Ammount of Booking
        /// </summary>
        public decimal G01F06 { get; set; }
    }
}
