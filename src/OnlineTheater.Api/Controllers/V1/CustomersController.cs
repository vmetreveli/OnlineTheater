using ErrorOr;
using OnlineTheater.Api.Infrastructure;
using OnlineTheater.Applications.Features.Customer.Queries.GetAllCustomers;
using OnlineTheater.Applications.Features.Customer.Queries.GetCustomers;
using OnlineTheater.Domains.Entities;
using OnlineTheater.Infrastructure.Repositories;


namespace OnlineTheater.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/[controller]")]
public sealed class CustomersController : ApiController
{
    // private readonly MovieRepository _movieRepository;
    // private readonly CustomerRepository _customerRepository;
    //
    // public CustomersController(MovieRepository movieRepository,
    //     CustomerRepository customerRepository)
    //
    // {
    //     _customerRepository = customerRepository;
    //     _movieRepository = movieRepository;
    // }


    public CustomersController(ISender mediator) : base(mediator)
    {
    }


    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetCustomerByIdQuery(id);
        var result = await Mediator.Send(query, cancellationToken);

        return result.MatchFirst<IActionResult>(
            value => Ok(value)
            , error => BadRequest());

    }

    [HttpGet]
    public async Task<IActionResult> GetList(CancellationToken cancellationToken)
    {
        var query = new GetAllCustomersQuery();
        var result = await Mediator.Send(query, cancellationToken);

        return result.MatchFirst<IActionResult>(
            value => Ok(value)
            , error => BadRequest());
    }
    //
    // [HttpPost]
    // public IActionResult Create([FromBody] CreateCustomerDto item)
    // {
    //     Result<CustomerName> customerNameOrError = CustomerName.Create(item.Name);
    //     Result<DomainErrors.Email> emailOrError = DomainErrors.Email.Create(item.Email);
    //
    //     Result result = Result.Combine(customerNameOrError, emailOrError);
    //     if (result.IsFailure)
    //         return Error(result.Error);
    //
    //     if (_customerRepository.GetByEmail(emailOrError.Value) != null)
    //         return Error("Email is already in use: " + item.Email);
    //
    //     var customer = new Customer(customerNameOrError.Value, emailOrError.Value);
    //     _customerRepository.Add(customer);
    //
    //     return Ok();
    // }
    //
    // [HttpPut]
    // [Route("{id}")]
    // public IActionResult Update(long id, [FromBody] UpdateCustomerDto item)
    // {
    //     Result<CustomerName> customerNameOrError = CustomerName.Create(item.Name);
    //     if (customerNameOrError.IsFailure)
    //         return Error(customerNameOrError.Error);
    //
    //     Customer customer = _customerRepository.GetById(id);
    //     if (customer == null)
    //         return Error("Invalid customer id: " + id);
    //
    //     customer.Name = customerNameOrError.Value;
    //
    //     return Ok();
    // }
    //
    // [HttpPost]
    // [Route("{id}/movies")]
    // public IActionResult PurchaseMovie(long id, [FromBody] long movieId)
    // {
    //     Movie movie = _movieRepository.GetById(movieId);
    //     if (movie == null)
    //         return Error("Invalid movie id: " + movieId);
    //
    //     Customer customer = _customerRepository.GetById(id);
    //     if (customer == null)
    //         return Error("Invalid customer id: " + id);
    //
    //     if (customer.HasPurchasedMovie(movie))
    //         return Error("The movie is already purchased: " + movie.Name);
    //
    //     customer.PurchaseMovie(movie);
    //
    //     return Ok();
    // }
    //
    // [HttpPost]
    // [Route("{id}/promotion")]
    // public IActionResult PromoteCustomer(long id)
    // {
    //     Customer customer = _customerRepository.GetById(id);
    //     if (customer == null)
    //         return Error("Invalid customer id: " + id);
    //
    //     Result promotionCheck = customer.CanPromote();
    //     if (promotionCheck.IsFailure)
    //         return Error(promotionCheck.Error);
    //
    //     customer.Promote();
    //
    //     return Ok();
    // }
}

