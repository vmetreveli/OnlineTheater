namespace OnlineTheater.Applications.Features.Customer.Commands.PurchaseMovie;

public sealed class PurchaseMovieCommandHandler : ICommandHandler<PurchaseMovieCommand, Unit>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMovieRepository _movieRepository;

    public PurchaseMovieCommandHandler(IMovieRepository movieRepository, ICustomerRepository customerRepository)
    {
        _movieRepository = movieRepository;
        _customerRepository = customerRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(PurchaseMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository.GetByIdAsync(request.MovieId, cancellationToken);
        if (movie == null) return Error.Failure(description: $"Invalid movie id: {request.MovieId}");

        var customer = await _customerRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (customer == null) return Error.Failure(description: $"Invalid customer id: {request.UserId}");

        if (customer.HasPurchasedMovie(movie))
            return Error.Conflict(description: $"The movie is already purchased:: {movie.Name}");

        customer.PurchaseMovie(movie);

        await _customerRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}