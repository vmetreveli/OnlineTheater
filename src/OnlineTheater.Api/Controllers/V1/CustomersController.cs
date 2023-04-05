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
    [Route("{userId:guid}")]
    public async Task<IActionResult> Get(Guid userId, CancellationToken cancellationToken)
    {
        var query = new GetCustomerByIdQuery(userId);
        var result = await Mediator.Send(query, cancellationToken);

        return result.MatchFirst<IActionResult>(
            Ok
            ,  _=>NotFound());
    }

    [HttpGet]
    public async Task<IActionResult> GetList(CancellationToken cancellationToken)
    {
        var query = new GetAllCustomersQuery();
        var result = await Mediator.Send(query, cancellationToken);

        return result.MatchFirst<IActionResult>(
            Ok
            , _ => NotFound());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return result.Match<IActionResult>(
            value => Ok(value)
            , BadRequest);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCustomerCommander command,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return result.Match<IActionResult>(
            value => Ok(value)
            , BadRequest);
    }

    [HttpPost]
    [Route("/movies")]
    public async Task<IActionResult> PurchaseMovie([FromBody]PurchaseMovieCommand command, CancellationToken cancellationToken)
    {

        var result = await Mediator.Send(command, cancellationToken);
        return result.MatchFirst<IActionResult>(
            value => Ok(value)
            , error => BadRequest(error));
    }


    [HttpPost]
    [Route("/promotion")]
    public async Task<IActionResult> PromoteCustomer(PromoteCustomerCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return result.MatchFirst<IActionResult>(
            value => Ok(value)
            , error => BadRequest(error));
    }
}