using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineTheater.Domains.Entities;
using OnlineTheater.Domains.Enums;

namespace OnlineTheater.Infrastructure.Context.Configurations;

internal sealed class MovieConfig : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("Movies");
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Name);
        builder.Property(x => x.LicensingModel);

        builder.HasDiscriminator(x=>x.LicensingModel)
            .HasValue<TwoDaysMovie>(LicensingModel.TwoDays)
            .HasValue<LifeLongMovie>(LicensingModel.LifeLong);


        builder.Property(c => c.CreatedOn).IsRequired();
        builder.Property(c => c.ModifiedOn);
        builder.Property(c => c.DeletedOn);
        builder.Property(c => c.IsDelete).IsRequired();

        builder.HasQueryFilter(c => !c.IsDelete);
    }
}

public sealed class TwoDaysMovieConfiguration : IEntityTypeConfiguration<TwoDaysMovie>
{
    public void Configure(EntityTypeBuilder<TwoDaysMovie> builder) =>
        builder.HasBaseType<Movie>();
}

public sealed class LifeLongMovieConfiguration : IEntityTypeConfiguration<LifeLongMovie>
{
    public void Configure(EntityTypeBuilder<LifeLongMovie> builder) =>
        builder.HasBaseType<Movie>();
}