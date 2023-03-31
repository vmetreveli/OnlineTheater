using OnlineTheater.Applications.Abstractions.Messaging;
using OnlineTheater.Applications.Contracts;
using OnlineTheater.Domains.Repository;

namespace OnlineTheater.Applications.Features.Customer.Queries.GetAllCustomers;

public sealed class GetAllCustomersQueryHandler : IQueryHandler<GetAllCustomersQuery, IQueryable<CustomerInListDto>>
{
    private readonly ICustomerRepository _customerRepository;


    public GetAllCustomersQueryHandler(ICustomerRepository customerRepository)
        => _customerRepository = customerRepository;

    public async Task<ErrorOr<IQueryable<CustomerInListDto>>> Handle(GetAllCustomersQuery request,
        CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.GetAllAsync(cancellationToken);
        if (!customers.Any())
        {
            return Error.NotFound();
        }

        return ErrorOr.ErrorOr.From(customers
            .Select(x => new CustomerInListDto
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                MoneySpent = x.MoneySpent,
                Status = x.Status.ToString(),
                StatusExpirationDate = x.StatusExpirationDate
            }));
    }
}