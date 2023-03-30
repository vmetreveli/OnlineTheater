using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OnlineTheater.Domains.Entities;
using Referendum.Domain.Errors;

namespace OnlineTheater.Infrastructure.Context;

public sealed class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }


    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }


    #region Entites

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<PurchasedMovie> PurchasedMovies { get; set; }
    #endregion
}