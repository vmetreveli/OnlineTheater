using OnlineTheater.Applications.Abstractions.Messaging;

namespace OnlineTheater.Applications.Features.Customer.Commands.PromoteCustomer;

public sealed class PromoteCustomerCommand : ICommand<Unit>
{
    public Guid UserId { get; set; }
}