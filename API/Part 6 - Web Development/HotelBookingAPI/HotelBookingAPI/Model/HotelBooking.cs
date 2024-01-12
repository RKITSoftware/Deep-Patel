namespace HotelBookingAPI.Model
{
    /// <summary>
    /// HotelBooking class represents a model for hotel booking information.
    /// </summary>
    public class HotelBooking
    {
        #region Public Properties

        /// <summary>
        /// Unique identifier for each booking.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Room number associated with the booking.
        /// </summary>
        public int RoomNumber { get; set; }

        /// <summary>
        /// Client's name who made the booking (nullable).
        /// </summary>
        public string? ClientName { get; set; }

        #endregion
    }
}
