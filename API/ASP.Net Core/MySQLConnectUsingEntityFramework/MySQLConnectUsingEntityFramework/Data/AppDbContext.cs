using Microsoft.EntityFrameworkCore;
using MySQLConnectUsingEntityFramework.Models;

namespace MySQLConnectUsingEntityFramework.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<UserData> UserData { get; set; }
    }
}
