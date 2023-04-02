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

        builder.OwnsOne(e => e.Price, modelNameBuilder =>
            modelNameBuilder
                .Property(l => l.Value)
                .HasColumnName(nameof(Customer.MoneySpent))
                .HasColumnType("decimal(18, 2)")
                .IsRequired());

        builder.Navigation(n => n.Price);

        builder.Property(m => m.PurchaseDate);
        builder.Property(m => m.ExpirationDate);
        builder.Property(m => m.MovieId);
        builder.Property(m => m.CustomerId);

        builder.HasOne(x => x.Movie)
            .WithMany().HasForeignKey(x => x.MovieId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.Property(c => c.CreatedOn).IsRequired();
        builder.Property(c => c.ModifiedOn);
        builder.Property(c => c.DeletedOn);
        builder.Property(c => c.IsDelete).IsRequired();

        builder.HasQueryFilter(c => !c.IsDelete);
    }
}