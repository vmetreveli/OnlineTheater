using OnlineTheater.Api.Infrastructure;
using OnlineTheater.Applications.Features.Customer.Commands.CreateCustomer;
using OnlineTheater.Applications.Features.Customer.Commands.PromoteCustomer;
using OnlineTheater.Applications.Features.Customer.Commands.PurchaseMovie;
using OnlineTheater.Applications.Features.Customer.Commands.UpdateCustomer;
using OnlineTheater.Applications.Features.Customer.Queries.GetAllCustomers;
using OnlineTheater.Applications.Features.Customer.Queries.GetCustomerById;

namespace OnlineTheater.Api.Controllers.V1;

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
    public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command, CancellationToken cancellationToken)
    {
    var result = await Mediator.Send(command, cancellationToken);

        return result.MatchFirst<IActionResult>(
            value => Ok(value)
            , error => BadRequest());
    }

    [HttpPut]
    //[Route()]
    public async Task<IActionResult> Update([FromBody] UpdateCustomerCommander command,
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


    [HttpPost]
    [Route("{id}/promotion")]
    public async Task<IActionResult> PromoteCustomer(Guid id, CancellationToken cancellationToken)
    {
        var command = new PromoteCustomerCommand
        {
            UserId = id
        };
        var result = await Mediator.Send(command, cancellationToken);
        return result.MatchFirst<IActionResult>(
            value => Ok(value)
            , error => BadRequest());
    }
}