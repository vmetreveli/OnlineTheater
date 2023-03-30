using ErrorOr;
using OnlineTheater.Api.Infrastructure;
using OnlineTheater.Applications.Features.Customer.Commands.CreateCustomer;
using OnlineTheater.Applications.Features.Customer.Commands.PurchaseMovie;
using OnlineTheater.Applications.Features.Customer.Commands.UpdateCustomer;
using OnlineTheater.Applications.Features.Customer.Queries.GetAllCustomers;
using OnlineTheater.Applications.Features.Customer.Queries.GetCustomers;
using OnlineTheater.Domains.Entities;
using OnlineTheater.Infrastructure.Repositories;


namespace OnlineTheater.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/[controller]")]
public sealed class CustomersController : ApiController
{
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
            , error => NotFound());
    }

    [HttpGet]
    public async Task<IActionResult> GetList(CancellationToken cancellationToken)
    {
        var query = new GetAllCustomersQuery();
        var result = await Mediator.Send(query, cancellationToken);

        return result.MatchFirst<IActionResult>(
            value => Ok(value)
            , error => NotFound());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Customer item, CancellationToken cancellationToken)
    {
        var command = new CreateCustomerCommand
        {
            Customer = item
        };
        var result = await Mediator.Send(command, cancellationToken);

        return result.MatchFirst<IActionResult>(
            value => Ok(value)
            , error => BadRequest());
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateCustomerCommander command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return result.MatchFirst<IActionResult>(
            value => Ok(value)
            , error => BadRequest());
    }

    [HttpPost]
    [Route("{id}/movies")]
    public async Task<IActionResult> PurchaseMovie(Guid id, [FromBody] Guid movieId,
        CancellationToken cancellationToken)
    {
        var command = new PurchaseMovieCommand
        {
            UserId = id,
            MovieId = movieId
        };
        var result = await Mediator.Send(command, cancellationToken);
        return result.MatchFirst<IActionResult>(
            value => Ok(value)
            , error => BadRequest());
    }

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