using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineTheater.Domains.Entities;

namespace OnlineTheater.Infrastructure.Context.Configurations;

internal sealed class MovieConfig : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("Movies");
        builder.HasKey(m => m.Id);
        
        builder.Property(m => m.Name);
        builder.Property(m => m.LicensingModel);
    }
}