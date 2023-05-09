using BeymenCase.Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeymenCase.Infrastructure.Persistence
{
    public class BeymenDbContext : DbContext
    {
        public DbSet<AppData> AppData { get; set; }
        public BeymenDbContext(DbContextOptions<BeymenDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<AppData>().HasData(
            //    new AppData
            //    {
            //        Id = Guid.NewGuid(),
            //    }
            //);
        }
    }
}
