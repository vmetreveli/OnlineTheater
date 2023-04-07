using Microsoft.EntityFrameworkCore;
using OnlineTheater.Domains.Entities;
using OnlineTheater.Domains.Repository;
using OnlineTheater.Domains.ValueObjects;
using OnlineTheater.Infrastructure.Specifications;

namespace OnlineTheater.Infrastructure.Repositories;

public sealed class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
{


    public CustomerRepository(DbContext dbContext):base(dbContext)
    {

    }

    public async Task<Customer?> GetByEmailAsync(Email email, CancellationToken cancellationToken) =>
        await ApplySpecification(new CustomerWithEmailSpecification(email))
            .FirstOrDefaultAsync(cancellationToken);
}