using BeymenCase.Library.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeymenCase.Infrastructure.Persistence.Configurations
{
    public class AppDataConfiguration : IEntityTypeConfiguration<AppData>
    {
        public void Configure(EntityTypeBuilder<AppData> builder)
        {
            builder.HasKey(x => x.Id);  
            builder.Property(s => s.Name).IsRequired().HasMaxLength(200);
            builder.Property(s => s.ApplicationName).IsRequired().HasMaxLength(250);
            builder.Property(s => s.Type).IsRequired().HasMaxLength(50);
            builder.Property(s => s.Value).IsRequired().HasMaxLength(250);
            builder.ToTable("AppData");
        }
    }
}
