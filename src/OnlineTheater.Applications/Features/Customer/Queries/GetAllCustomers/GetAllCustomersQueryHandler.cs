using System.Runtime.InteropServices.JavaScript;
using OnlineTheater.Applications.Abstractions.Messaging;
using OnlineTheater.Domains.Repository;

namespace OnlineTheater.Applications.Features.Customer.Queries.GetAllCustomers;

public sealed class
    GetAllCustomersQueryHandler : IQueryHandler<GetAllCustomersQuery, IQueryable<Domains.Entities.Customer>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetAllCustomersQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<ErrorOr<IQueryable<Domains.Entities.Customer>>> Handle(GetAllCustomersQuery request,
        CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.GetAllAsync(cancellationToken);
        if (!customers.Any())
        {
            return Error.NotFound();
        }

        return ErrorOr.ErrorOr.From(customers);
    }
}