using Microsoft.EntityFrameworkCore;
using HotelBookingAPI.Model;

namespace HotelBookingAPI.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<HotelBooking> Bookings { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
            
        }
    }
}
