namespace OnlineTheater.Applications.Features.Customer.Commands.PromoteCustomer;

public sealed record PromoteCustomerCommand(Guid UserId ) : ICommand<Unit>;