using OnlineTheater.Domains.Entities;

namespace OnlineTheater.Domains.Repository;

public interface ICustomerRepository:IRepositoryBase<Customer>
{
    Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}