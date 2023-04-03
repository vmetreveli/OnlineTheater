namespace OnlineTheater.Applications.Features.Customer.Commands.CreateCustomer;

public sealed class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand, Unit>
{
    private readonly ICustomerRepository _customerRepository;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
        => _customerRepository = customerRepository;

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

        var customer = await _customerRepository.GetByEmailAsync(emailOrError.Value, cancellationToken);

        if (customer is not null) return Error.Failure(description: $"Email is already in use: {request.Email}");

        customer = new Domains.Entities.Customer(customerNameOrError.Value, emailOrError.Value);


        await _customerRepository.CreateAsync(customer, cancellationToken);
        await _customerRepository.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}