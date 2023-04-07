using OnlineTheater.Applications.Contracts;

namespace OnlineTheater.Applications.Features.Customer.Queries.GetAllCustomers;

public sealed class GetAllCustomersQueryHandler : IQueryHandler<GetAllCustomersQuery, IQueryable<CustomerInListDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCustomersQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;


    public async Task<ErrorOr<IQueryable<CustomerInListDto>>> Handle(GetAllCustomersQuery request,
        CancellationToken cancellationToken)
    {
        var customers = await _unitOfWork.Customer.GetAllAsync(cancellationToken);
        if (!customers.Any()) return Error.NotFound();

        return ErrorOr.ErrorOr.From(customers
            .Select(x => new CustomerInListDto
            {
                Id = x.Id,
                Name = x.Name!,
                Email = x.Email!,
                MoneySpent = x.MoneySpent!,
                Status = x.Status!.Type.ToString(),
                StatusExpirationDate = x.Status.ExpirationDate!.Date,
            }));
    }
}