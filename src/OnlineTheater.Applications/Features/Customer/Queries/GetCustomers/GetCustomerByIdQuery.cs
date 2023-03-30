using OnlineTheater.Applications.Abstractions.Messaging;
using OnlineTheater.Applications.Contracts;

namespace OnlineTheater.Applications.Features.Customer.Queries.GetCustomers;

public sealed record GetCustomerByIdQuery(
    Guid QuestionId
) : IQuery<Domains.Entities.Customer>;