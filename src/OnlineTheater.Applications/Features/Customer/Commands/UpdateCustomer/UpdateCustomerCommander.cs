using OnlineTheater.Applications.Abstractions.Messaging;
using OnlineTheater.Domains.ValueObjects;

namespace OnlineTheater.Applications.Features.Customer.Commands.UpdateCustomer;

public sealed class UpdateCustomerCommander:ICommand<Unit>
{
    public string Name { get; set; }
    public Guid Id { get; set; }
}