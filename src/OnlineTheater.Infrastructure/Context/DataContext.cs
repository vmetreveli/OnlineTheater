using System.Reflection;
using Referendum.Domain.Entities;

namespace Referendum.Infrastructure.Context;

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

    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<BasicInfo> BasicInfos { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }

    #endregion
}