using OnlineTheater.Applications.Abstractions.Messaging;
using OnlineTheater.Domains.Repository;

namespace OnlineTheater.Applications.Features.Customer.Commands.UpdateCustomer;

public sealed class UpdateCustomerCommanderHandler : ICommandHandler<UpdateCustomerCommander, Unit>
{
    private readonly ICustomerRepository _customerRepository;

    public UpdateCustomerCommanderHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(UpdateCustomerCommander request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id, cancellationToken);
        if (customer == null)
        {
            return Error.Failure(description: $"Invalid customer id:: {request.Id}");
        }

        customer.Name = request.Customer.Name;
        _customerRepository.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}