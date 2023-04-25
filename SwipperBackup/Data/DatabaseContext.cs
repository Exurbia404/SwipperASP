using Microsoft.EntityFrameworkCore;
using SwipperBackup.Models;

namespace SwipperBackup.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Listing> Listings { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
