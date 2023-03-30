using OnlineTheater.Applications.Abstractions.Messaging;
using OnlineTheater.Domains.Repository;
using Referendum.Domain.Enums;

namespace OnlineTheater.Applications.Features.Customer.Commands.CreateCustomer;

public sealed class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand, Unit>
{
    private readonly ICustomerRepository _customerRepository;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        if (_customerRepository.GetByEmail(request.Customer.Email) != null)
        {
            return Error.Failure(description: $"Email is already in use: {request.Customer.Email}");
        }

        // request.customer.Id = null;
        request.Customer.Status = CustomerStatus.Regular;
        _customerRepository.CreateAsync(request.Customer, cancellationToken);
        _customerRepository.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}