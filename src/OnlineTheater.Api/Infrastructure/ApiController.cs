namespace OnlineTheater.Api.Infrastructure;

[ApiController]
//[Authorize]
public class ApiController : ControllerBase
{
    protected ApiController(ISender mediator) => Mediator = mediator;
    protected ISender Mediator { get; }
}