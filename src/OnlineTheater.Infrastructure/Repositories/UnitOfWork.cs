using OnlineTheater.Domains.Repository;
using OnlineTheater.Infrastructure.Context;
using static System.GC;

namespace OnlineTheater.Infrastructure.Repositories;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _dbContext;
    private ICustomerRepository _customer;
    private IMovieRepository _movie;


    public UnitOfWork(DataContext dbContext) => _dbContext = dbContext;

    public ICustomerRepository Customer => _customer ??= new CustomerRepository(_dbContext);
    public IMovieRepository Movie => _movie ??= new MovieRepository(_dbContext);


    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) =>
        await _dbContext.SaveChangesAsync(cancellationToken);

    public void Dispose()
    {
        Dispose(true);
        SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (disposing) _dbContext.Dispose();
    }
}