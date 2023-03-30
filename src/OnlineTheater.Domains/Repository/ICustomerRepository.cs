using OnlineTheater.Domains.Entities;

namespace OnlineTheater.Domains.Repository;

public interface ICustomerRepository:IRepositoryBase<Customer>
{
    public Customer? GetByEmail(string email);
}