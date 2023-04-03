using OnlineTheater.Applications.Contracts;

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
        if (!customers.Any()) return Error.NotFound();

        return ErrorOr.ErrorOr.From(customers
            .Select(x => new CustomerInListDto
            {
                Id = x.Id,
                Name = x.Name.Value,
                Email = x.Email.Value,
                MoneySpent = x.MoneySpent.Value,
                Status = x.Status.ToString(),
                StatusExpirationDate = x.StatusExpirationDate
            }));
    }
}