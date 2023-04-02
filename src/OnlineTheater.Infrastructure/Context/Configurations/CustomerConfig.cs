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

        builder.Navigation(n => n.Name);

        builder.OwnsOne(e => e.Email, modelNameBuilder =>
            modelNameBuilder
                .Property(l => l.Value)
                .HasColumnName(nameof(Customer.Email))
                .IsRequired());

        builder.Navigation(n => n.Email);




        // builder.Property(c => c.Name.Value)
        //     .HasColumnName(nameof(Customer.Name));
       // builder.Navigation(c => c.Email);
        builder.Property(c => c.Status);
        builder.Property(c => c.StatusExpirationDate);
        builder.Property(c => c.MoneySpent)
            .HasColumnType("decimal(18, 2)");

        builder.HasMany(x => x.PurchasedMovies);

        builder.Property(c => c.CreatedOn).IsRequired();
        builder.Property(c => c.ModifiedOn);
        builder.Property(c => c.DeletedOn);
        builder.Property(c => c.IsDelete).IsRequired();

        builder.HasQueryFilter(c => !c.IsDelete);
    }
}