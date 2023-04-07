namespace OnlineTheater.Applications.Features.Customer.Commands.UpdateCustomer;

public sealed class UpdateCustomerCommanderHandler : ICommandHandler<UpdateCustomerCommander, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCustomerCommanderHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;


    public async Task<ErrorOr<Unit>> Handle(UpdateCustomerCommander request, CancellationToken cancellationToken)
    {
        var customerNameOrError = CustomerName.Create(request.Name);

        if (customerNameOrError.IsError) return customerNameOrError.Errors;

        var customer = await _unitOfWork.Customer.GetByIdAsync(request.Id, cancellationToken);
        if (customer == null) return Error.Failure(description: $"Invalid customer id:: {request.Id}");

        // customer.Name = customerNameOrError.Value;
        customer.UpdateCustomer(customerNameOrError.Value);
       await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}