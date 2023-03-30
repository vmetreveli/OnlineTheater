using Microsoft.EntityFrameworkCore;
using OnlineTheater.Domains.Entities;
using OnlineTheater.Domains.Repository;

namespace OnlineTheater.Infrastructure.Repositories;

public sealed class CustomerRepository:RepositoryBase<Customer>, ICustomerRepository
{
    private readonly DbContext _dbContext;
    public CustomerRepository(DbContext context, DbContext dbContext) : base(context)
    {
        _dbContext = dbContext;
    }

    public Customer? GetByEmail(string email) =>
        _dbContext.Set<Customer>()
            .SingleOrDefault(x => x.Email == email);
}