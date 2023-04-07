namespace OnlineTheater.Domains.Repository;

public interface IUnitOfWork
{
    ICustomerRepository Customer { get; }
    IMovieRepository Movie { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}