using OnlineTheater.Applications.Abstractions.Messaging;
using OnlineTheater.Applications.Contracts;

namespace OnlineTheater.Applications.Features.Customer.Queries.GetAllCustomers;

public sealed record GetAllCustomersQuery : IQuery<IQueryable<CustomerInListDto>>;