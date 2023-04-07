using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OnlineTheater.Domains.Primitives;
using OnlineTheater.Domains.Primitives.Specifications;
using OnlineTheater.Domains.Repository;

namespace OnlineTheater.Infrastructure.Repositories;

public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
{
    private readonly DbContext _context;

    protected RepositoryBase(DbContext context) => _context = context;

    public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default) =>
        await _context.Set<TEntity>().AddAsync(entity, cancellationToken);

    public async Task AddRangeAsync(IEnumerable<TEntity> entity, CancellationToken cancellationToken) =>
        await _context.Set<TEntity>().AddRangeAsync(entity, cancellationToken);

    public Task Update(TEntity entity) =>
        Task.FromResult(_context.Set<TEntity>().Update(entity));

    public Task Delete(TEntity entity)
    {
        var res = _context.Set<TEntity>()
            .Remove(entity);
        return Task.CompletedTask;
    }

    public Task<IQueryable<TEntity>> SearchFor(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default) =>
        Task.FromResult(_context.Set<TEntity>().AsNoTracking().Where(predicate));

    public async Task<IQueryable<TEntity>> GetAllAsync(CancellationToken cancellationToken) =>
        _context.Set<TEntity>().AsNoTracking();

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);


    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await _context.SaveChangesAsync(cancellationToken);

    public IQueryable<TEntity> ApplySpecification(Specification<TEntity> specification) =>
        SpecificationEvaluator.GetQuery(_context.Set<TEntity>(), specification);
}