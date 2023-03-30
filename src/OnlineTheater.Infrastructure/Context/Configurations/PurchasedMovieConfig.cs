using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineTheater.Domains.Entities;

namespace OnlineTheater.Infrastructure.Context.Configurations;

internal sealed class PurchasedMovieConfig : IEntityTypeConfiguration<PurchasedMovie>
{
    public void Configure(EntityTypeBuilder<PurchasedMovie> builder)
    {
        builder.ToTable("PurchasedMovies");
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Price);
        builder.Property(m => m.PurchaseDate);
        builder.Property(m => m.ExpirationDate);
        builder.Property(m => m.MovieId);
        builder.Property(m => m.CustomerId);

       builder.Navigation(x => x.Movie);
    }
}