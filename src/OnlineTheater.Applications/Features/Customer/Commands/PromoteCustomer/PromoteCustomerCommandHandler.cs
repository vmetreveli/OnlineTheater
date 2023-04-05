namespace OnlineTheater.Applications.Features.Customer.Commands.PromoteCustomer;

public sealed class PromoteCustomerCommandHandler : ICommandHandler<PromoteCustomerCommand, Unit>
{
    private readonly ICustomerRepository _customerRepository;

    public PromoteCustomerCommandHandler(ICustomerRepository customerRepository) =>
        _customerRepository = customerRepository;

    public async Task<ErrorOr<Unit>> Handle(PromoteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (customer == null)
            return Error.Failure(description: $"Invalid customer id: {request.UserId}");

        if (customer.Status.IsAdvance)
            return Error.Conflict(description:"The customer already has the Advanced status");

        var success = customer.Promote();
        if (!success) return Error.Conflict(description:"Cannot promote the customer");

        await _customerRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}