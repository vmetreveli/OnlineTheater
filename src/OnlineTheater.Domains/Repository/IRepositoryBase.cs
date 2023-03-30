using System.Linq.Expressions;
using Referendum.Domain.Primitives.Specifications;

namespace Referendum.Domain.Repository;

public interface IRepositoryBase<T> where T : EntityBase
{
    Task CreateAsync(T entity, CancellationToken cancellationToken);
    Task AddRangeAsync(IEnumerable<T> entity, CancellationToken cancellationToken);
    Task Delete(T entity);
    Task Update(T entity);
    Task<IQueryable<T>> SearchFor(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    Task<IQueryable<T>> GetAllAsync(CancellationToken cancellationToken);
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    IQueryable<T> ApplySpecification(Specification<T> specification);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}