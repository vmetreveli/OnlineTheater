﻿using OnlineTheater.Applications.Contracts;

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
             Name = res.Name.Value,
             Email = res.Email.Value,
             MoneySpent = res.MoneySpent.Value,
             Status = res.Status.Type.ToString(),
             StatusExpirationDate = res.Status?.ExpirationDate?.Date,
            PurchasedMovies = res.PurchasedMovies
                .Select(x => new PurchasedMovieDto
                {
                    Price = x?.Price?.Value,
                     ExpirationDate = x.ExpirationDate.Date,
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