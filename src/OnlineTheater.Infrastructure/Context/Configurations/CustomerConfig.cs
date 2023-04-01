using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineTheater.Domains.Entities;
using OnlineTheater.Domains.Errors;

namespace OnlineTheater.Infrastructure.Context.Configurations;

internal sealed class CustomerConfig : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");
        builder.HasKey(customer => customer.Id);

        builder.OwnsOne(e => e.Name, modelNameBuilder =>
            modelNameBuilder
                .Property(l => l.Value)
                .HasColumnName(nameof(Customer.Name))
                .IsRequired());
        builder.Navigation(e => e.Name);

        builder.OwnsOne(e => e.Email, modelNameBuilder =>
            modelNameBuilder
                .Property(l => l.Value)
                .HasColumnName(nameof(Customer.Email))
                .IsRequired());
        builder.Navigation(e => e.Name);

        builder.Property(c => c.Status);
        builder.Property(c => c.StatusExpirationDate);
        builder.Property(c => c.MoneySpent)
            .HasColumnType("decimal(18, 2)");

        builder.HasMany(x => x.PurchasedMovies);
    }
}