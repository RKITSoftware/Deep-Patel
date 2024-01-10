using Microsoft.EntityFrameworkCore;
using HotelBookingAPI.Model;

namespace HotelBookingAPI.Data
{
    /// <summary>
    /// ApiContext class represents the database context for the hotel booking application.
    /// </summary>
    public class ApiContext : DbContext
    {
        #region Public Properties

        /// <summary>
        /// DbSet property to represent the "Bookings" table in the database.
        /// </summary>
        public DbSet<HotelBooking> Bookings { get; set; }

        #endregion

        #region Constructor

        // Constructor to initialize the ApiContext with DbContextOptions.
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
            // The constructor takes DbContextOptions as a parameter, which allows configuring the database connection.
        }

        #endregion
    }
}
