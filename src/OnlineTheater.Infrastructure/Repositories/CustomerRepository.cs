using Microsoft.EntityFrameworkCore;
using OnlineTheater.Domains.Entities;
using OnlineTheater.Domains.Repository;
using OnlineTheater.Domains.ValueObjects;

namespace OnlineTheater.Infrastructure.Repositories;

public sealed class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
{
    private readonly DbContext _dbContext;

    public CustomerRepository(DbContext dbContext) : base(dbContext)
        => _dbContext = dbContext;

    public async Task<Customer?> GetByEmailAsync(Email email, CancellationToken cancellationToken) =>
        await _dbContext.Set<Customer>()
            .FirstOrDefaultAsync(x => x.Email.Value == email.Value, cancellationToken);
}