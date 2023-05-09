using BeymenCase.Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeymenCase.Library.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<AppData> AppData { get; set; }
        public ApplicationDbContext(string databaseConnection) : base()
        {
            ConnectionString = databaseConnection.ToString();
        }

        public string ConnectionString { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
