using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineTheater.Domains.Entities;

namespace OnlineTheater.Infrastructure.Context.Configurations;

internal sealed class PurchasedMovieConfig : IEntityTypeConfiguration<PurchasedMovie>
{
    public void Configure(EntityTypeBuilder<PurchasedMovie> builder)
    {
        builder.ToTable("PurchasedMovies");

        builder.HasKey(x => x.Id);

       // builder.Property(x => x.Price);

        builder.OwnsOne(e => e.Price, modelNameBuilder =>
            modelNameBuilder
                .Property(l => l.Value)
                .HasColumnName(nameof(PurchasedMovie.Price)));

        builder.Property(x => x.PurchaseDate);

        // builder.Property(x => x.ExpirationDate);


        builder.OwnsOne(e => e.ExpirationDate, modelNameBuilder =>
            modelNameBuilder
                .Property(l => l.Date)
                .HasColumnName(nameof(PurchasedMovie.ExpirationDate)));

        builder.HasOne(x => x.Movie)
            .WithMany()
            .HasForeignKey(x => x.MovieId);

        builder.HasOne(x => x.Customer)
            .WithMany(x => x.PurchasedMovies)
            .HasForeignKey(x => x.CustomerId);




        builder.Property(c => c.CreatedOn).IsRequired();
        builder.Property(c => c.ModifiedOn);
        builder.Property(c => c.DeletedOn);
        builder.Property(c => c.IsDelete).IsRequired();

        builder.HasQueryFilter(c => !c.IsDelete);
    }
}