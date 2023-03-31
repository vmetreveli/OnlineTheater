using OnlineTheater.Applications.Abstractions.Messaging;
using OnlineTheater.Applications.Contracts;

namespace OnlineTheater.Applications.Features.Customer.Queries.GetCustomerById;

public sealed record GetCustomerByIdQuery(
    Guid QuestionId
) : IQuery<CustomerDto>;