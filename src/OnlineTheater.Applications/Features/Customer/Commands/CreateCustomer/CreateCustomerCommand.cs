namespace OnlineTheater.Applications.Features.Customer.Commands.CreateCustomer;

public sealed record CreateCustomerCommand(string Name,string Email) : ICommand<Unit>;