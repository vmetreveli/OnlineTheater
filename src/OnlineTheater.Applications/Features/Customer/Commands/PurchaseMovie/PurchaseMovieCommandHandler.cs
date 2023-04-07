namespace OnlineTheater.Applications.Features.Customer.Commands.PurchaseMovie;

public sealed class PurchaseMovieCommandHandler : ICommandHandler<PurchaseMovieCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public PurchaseMovieCommandHandler(IUnitOfWork unitOfWork) =>
        _unitOfWork = unitOfWork;

    public async Task<ErrorOr<Unit>> Handle(PurchaseMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await _unitOfWork.Movie.GetByIdAsync(request.MovieId, cancellationToken);
        if (movie == null) return Error.Failure(description: $"Invalid movie id: {request.MovieId}");

        var customer = await _unitOfWork.Customer.GetByIdAsync(request.UserId, cancellationToken);
        if (customer == null) return Error.Failure(description: $"Invalid customer id: {request.UserId}");

        if (customer.HasPurchasedMovie(movie))
            return Error.Conflict(description: $"The movie is already purchased:: {movie.Name}");

        customer.PurchaseMovie(movie);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}