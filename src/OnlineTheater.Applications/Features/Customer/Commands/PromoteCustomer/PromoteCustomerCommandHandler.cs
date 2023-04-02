using OnlineTheater.Applications.Abstractions.Messaging;
using OnlineTheater.Domains.Enums;
using OnlineTheater.Domains.Repository;
using OnlineTheater.Domains.Services;

namespace OnlineTheater.Applications.Features.Customer.Commands.PromoteCustomer;

public sealed class PromoteCustomerCommandHandler : ICommandHandler<PromoteCustomerCommand, Unit>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICustomerService _customerService;

    public PromoteCustomerCommandHandler(ICustomerRepository customerRepository, ICustomerService customerService)
    {
        _customerRepository = customerRepository;
        _customerService = customerService;
    }

    public async Task<ErrorOr<Unit>> Handle(PromoteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (customer == null)
            return Error.Failure(description: $"Invalid customer id: {request.UserId}");

        if (customer is { Status: CustomerStatus.Advanced, StatusExpirationDate.IsExpired: false })
            return Error.Conflict("The customer already has the Advanced status");

        var success = _customerService.PromoteCustomer(customer);
        if (!success) return Error.Conflict("Cannot promote the customer");

        await _customerRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}