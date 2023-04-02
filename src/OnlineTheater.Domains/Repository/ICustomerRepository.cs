using OnlineTheater.Domains.Entities;
using OnlineTheater.Domains.ValueObjects;

namespace OnlineTheater.Domains.Repository;

public interface ICustomerRepository : IRepositoryBase<Customer>
{
    Task<Customer?> GetByEmailAsync(Email email, CancellationToken cancellationToken);
}