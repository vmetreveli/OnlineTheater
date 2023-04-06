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

        var promotionCheck = customer.CanPromote();
        if (promotionCheck.IsError)
            return promotionCheck.Errors;

        customer.Promote();


        await _customerRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}