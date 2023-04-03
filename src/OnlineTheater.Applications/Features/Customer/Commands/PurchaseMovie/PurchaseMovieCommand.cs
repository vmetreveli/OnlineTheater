namespace OnlineTheater.Applications.Features.Customer.Commands.PurchaseMovie;

public sealed class PurchaseMovieCommand : ICommand<Unit>
{
    public Guid MovieId { get; set; }
    public Guid UserId { get; set; }
}