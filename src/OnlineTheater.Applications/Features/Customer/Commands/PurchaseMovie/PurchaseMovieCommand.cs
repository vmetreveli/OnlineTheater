namespace OnlineTheater.Applications.Features.Customer.Commands.PurchaseMovie;

public sealed record PurchaseMovieCommand(Guid MovieId,Guid UserId) : ICommand<Unit>;