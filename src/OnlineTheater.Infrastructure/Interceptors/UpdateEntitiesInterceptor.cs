using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Referendum.Domain.Primitives;

namespace Referendum.Infrastructure.Interceptors;

public sealed class UpdateEntitiesInterceptor : SaveChangesInterceptor
{
    /// <summary>
    ///     Updates the entities implementing <see cref="IAuditableEntity" /> interface.
    /// </summary>
    /// <param name="dbContext"></param>
    private static void UpdateAuditableEntities(DbContext dbContext, DateTime utcNow)
    {
        foreach (var entityEntry in dbContext.ChangeTracker.Entries<EntityBase>())
        {
            if (entityEntry.State == EntityState.Added)
                entityEntry.Property(nameof(EntityBase.CreatedOn)).CurrentValue = utcNow;

            if (entityEntry.State == EntityState.Modified)
                entityEntry.Property(nameof(EntityBase.ModifiedOn)).CurrentValue = utcNow;
        }
    }


    private static void UpdateDeletableEntities(DbContext dbContext, DateTime utcNow)
    {
        foreach (var entityEntry in dbContext.ChangeTracker.Entries<EntityBase>())
        {
            if (entityEntry.State != EntityState.Deleted) continue;

            entityEntry.Property(nameof(EntityBase.DeletedOn)).CurrentValue = utcNow;

            entityEntry.Property(nameof(EntityBase.IsDelete)).CurrentValue = true;

            entityEntry.State = EntityState.Modified;

            UpdateDeletedEntityEntryReferencesToUnchanged(entityEntry);
        }
    }

    private static void UpdateDeletedEntityEntryReferencesToUnchanged(EntityEntry entityEntry)
    {
        if (!entityEntry.References.Any()) return;

        foreach (var referenceEntry in entityEntry.References.Where(r =>
                     r.TargetEntry!.State == EntityState.Deleted))
        {
            referenceEntry.TargetEntry!.State = EntityState.Unchanged;

            UpdateDeletedEntityEntryReferencesToUnchanged(referenceEntry.TargetEntry);
        }
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;
        if (dbContext is null) return base.SavingChangesAsync(eventData, result, cancellationToken);

        var utcNow = DateTime.UtcNow;

        UpdateAuditableEntities(dbContext, utcNow);
        UpdateDeletableEntities(dbContext, utcNow);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}