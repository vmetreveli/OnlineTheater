using OnlineTheater.Applications.Abstractions.Messaging;

namespace OnlineTheater.Applications.Features.Customer.Commands.UpdateCustomer;

public sealed class UpdateCustomerCommander:ICommand<Unit>
{
    public Domains.Entities.Customer Customer { get; set; }
    public Guid Id { get; set; }
}