using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineTheater.Domains.Entities;

namespace OnlineTheater.Infrastructure.Context.Configurations;

internal sealed class CustomerConfig : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");
        builder.HasKey(customer => customer.Id);
        builder.Property(c => c.Name);
        builder.Property(c => c.Email);
        builder.Property(c => c.Status);
        builder.Property(c => c.StatusExpirationDate);
        builder.Property(c => c.MoneySpent);

        builder.HasMany(x => x.PurchasedMovies);
    }
}