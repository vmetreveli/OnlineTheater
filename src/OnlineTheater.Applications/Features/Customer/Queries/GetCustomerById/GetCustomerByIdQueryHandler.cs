using OnlineTheater.Applications.Abstractions.Messaging;
using OnlineTheater.Applications.Contracts;
using OnlineTheater.Domains.Repository;

namespace OnlineTheater.Applications.Features.Customer.Queries.GetCustomerById;

public sealed class GetCustomerByIdQueryHandler : IQueryHandler<GetCustomerByIdQuery, CustomerDto>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<CustomerDto>> Handle(GetCustomerByIdQuery request,
        CancellationToken cancellationToken)
    {
        var res = await _customerRepository
            .GetByIdAsync(request.QuestionId, cancellationToken);

        if (res is null) return Error.NotFound();

        var dto = new CustomerDto
        {
            Id = res.Id,
            Name = res.Name,
            Email = res.Email,
            MoneySpent = res.MoneySpent,
            Status = res.Status.ToString(),
            StatusExpirationDate = res.StatusExpirationDate,
            PurchasedMovies = res.PurchasedMovies
                .Select(x => new PurchasedMovieDto
                {
                    Price = x.Price,
                    ExpirationDate = x.ExpirationDate,
                    PurchaseDate = x.PurchaseDate,
                    Movie = new MovieDto
                    {
                        Id = x.Movie.Id,
                        Name = x.Movie.Name
                    }
                })
        };
        return dto;
    }
}