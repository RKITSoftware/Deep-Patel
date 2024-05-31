using Mail_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Mail_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Mail> Mails { get; set; }
    }
}
