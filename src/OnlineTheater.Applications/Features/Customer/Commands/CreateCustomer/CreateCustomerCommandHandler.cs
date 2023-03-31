using OnlineTheater.Applications.Abstractions.Messaging;
using OnlineTheater.Domains.Enums;
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
        if (await _customerRepository.GetByEmailAsync(request.Email, cancellationToken) != null)
        {
            return Error.Failure(description: $"Email is already in use: {request.Email}");
        }

        var customer = new Domains.Entities.Customer
        {
            Name = request.Name,
            Email = request.Email,
            MoneySpent = 0,
            Status = CustomerStatus.Regular,
            StatusExpirationDate = null
        };


        _customerRepository.CreateAsync(customer, cancellationToken);
        _customerRepository.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}