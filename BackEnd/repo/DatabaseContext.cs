using System.Data.Entity;
using BackEnd.Entity;
using BackEnd.Properties;

namespace BackEnd.db
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<VideoGame> VideoGames { get; set; }
        public DbSet<Laptop> Laptops { get; set; }

        public DatabaseContext()
            : base(Settings.Default.DbConnect)
        {
        }

        public static DatabaseContext SingletonDbContext = new DatabaseContext();
    }
}