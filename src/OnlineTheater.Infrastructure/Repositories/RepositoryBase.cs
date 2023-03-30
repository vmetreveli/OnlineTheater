using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OnlineTheater.Domains.Primitives;
using OnlineTheater.Domains.Primitives.Specifications;
using OnlineTheater.Domains.Repository;

namespace OnlineTheater.Infrastructure.Repositories;

public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
{
    private readonly DbContext _context;

    protected RepositoryBase(DbContext context) => _context = context;

    public async Task CreateAsync(T entity, CancellationToken cancellationToken = default) =>
        await _context.Set<T>().AddAsync(entity, cancellationToken);

    public async Task AddRangeAsync(IEnumerable<T> entity, CancellationToken cancellationToken) =>
        await _context.Set<T>().AddRangeAsync(entity, cancellationToken);

    public Task Update(T entity) =>
        Task.FromResult(_context.Set<T>().Update(entity));

    public Task Delete(T entity)
    {
        var res = _context.Set<T>()
            .Remove(entity);
        return Task.CompletedTask;
    }

    public Task<IQueryable<T>> SearchFor(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default) =>
        Task.FromResult(_context.Set<T>().AsNoTracking().Where(predicate));

    public async Task<IQueryable<T>> GetAllAsync(CancellationToken cancellationToken) =>
        _context.Set<T>().AsNoTracking();

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _context.Set<T>().FindAsync(new object[] {id}, cancellationToken);


    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await _context.SaveChangesAsync(cancellationToken);

    public IQueryable<T> ApplySpecification(Specification<T> specification) =>
        SpecificationEvaluator.GetQuery(_context.Set<T>(), specification);
}