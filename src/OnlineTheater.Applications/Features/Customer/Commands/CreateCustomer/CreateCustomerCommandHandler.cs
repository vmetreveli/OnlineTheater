namespace OnlineTheater.Applications.Features.Customer.Commands.CreateCustomer;

public sealed class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomerCommandHandler(IUnitOfWork unitOfWork) =>
        _unitOfWork = unitOfWork;

    public async Task<ErrorOr<Unit>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerNameOrError = CustomerName.Create(request.Name);
        var emailOrError = Email.Create(request.Email);

        if (emailOrError.IsError || customerNameOrError.IsError)
        {
            var errors = new List<Error>();
            errors.AddRange(emailOrError.ErrorsOrEmptyList);
            errors.AddRange(customerNameOrError.ErrorsOrEmptyList);
            return errors;
        }

        var customer = await _unitOfWork.Customer.GetByEmailAsync(emailOrError.Value, cancellationToken);

        if (customer is not null) return Error.Failure(description: $"Email is already in use: {request.Email}");

        customer = new Domains.Entities.Customer(customerNameOrError.Value, emailOrError.Value);


        await _unitOfWork.Customer.CreateAsync(customer, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}