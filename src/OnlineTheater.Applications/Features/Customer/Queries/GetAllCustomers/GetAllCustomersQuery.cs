using OnlineTheater.Applications.Abstractions.Messaging;

namespace OnlineTheater.Applications.Features.Customer.Queries.GetAllCustomers;

public sealed record GetAllCustomersQuery: IQuery<IQueryable<Domains.Entities.Customer>>;