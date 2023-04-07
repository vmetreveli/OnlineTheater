namespace OnlineTheater.Applications.Features.Customer.Commands.PromoteCustomer;

public sealed class PromoteCustomerCommandHandler : ICommandHandler<PromoteCustomerCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public PromoteCustomerCommandHandler(IUnitOfWork unitOfWork) =>
        _unitOfWork = unitOfWork;


    public async Task<ErrorOr<Unit>> Handle(PromoteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Customer.GetByIdAsync(request.UserId, cancellationToken);
        if (customer == null)
            return Error.Failure(description: $"Invalid customer id: {request.UserId}");

        var promotionCheck = customer.CanPromote();
        if (promotionCheck.IsError)
            return promotionCheck.Errors;

        customer.Promote();


        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}