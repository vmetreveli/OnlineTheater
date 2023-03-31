using OnlineTheater.Applications.Abstractions.Messaging;
using OnlineTheater.Applications.Contracts;

namespace OnlineTheater.Applications.Features.Customer.Commands.CreateCustomer;

public sealed class CreateCustomerCommand : ICommand<Unit>
{
    public string Name { get; set; }
    public string Email { get; set; }
}