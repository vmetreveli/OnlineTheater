using OnlineTheater.Applications.Abstractions.Messaging;
using OnlineTheater.Applications.Contracts;

namespace OnlineTheater.Applications.Features.Customer.Commands.CreateCustomer;

public sealed class CreateCustomerCommand : ICommand<Unit>
{
    public Domains.Entities.Customer Customer { get; set; }
}