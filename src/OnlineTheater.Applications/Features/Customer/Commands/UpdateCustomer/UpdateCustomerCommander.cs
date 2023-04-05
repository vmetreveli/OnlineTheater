namespace OnlineTheater.Applications.Features.Customer.Commands.UpdateCustomer;

public sealed record UpdateCustomerCommander(string Name,Guid Id) : ICommand<Unit>;