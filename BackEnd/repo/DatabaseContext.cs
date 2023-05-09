using System.Data.Entity;
using BackEnd.Properties;

namespace BackEnd.db
{
    public class DatabaseContext<T> : DbContext where T : Entity.Entity
    {
        
        public DbSet<T> Entities { get; set; }

        public DatabaseContext()
            : base(Settings.Default.DbConnect)
        {
        }

    }
}