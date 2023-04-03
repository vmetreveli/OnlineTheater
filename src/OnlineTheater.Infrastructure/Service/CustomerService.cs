using OnlineTheater.Domains.Entities;
using OnlineTheater.Domains.Enums;
using OnlineTheater.Domains.Services;
using OnlineTheater.Domains.ValueObjects;
using Referendum.Domain.Enums;

namespace OnlineTheater.Infrastructure.Service;

public class CustomerService : ICustomerService
{
    private readonly IMovieService _movieService;

    public CustomerService(IMovieService movieService) => _movieService = movieService;

    public Dollars CalculatePrice(CustomerStatus status, LicensingModel licensingModel)
    {
        var price = licensingModel switch
        {
            LicensingModel.TwoDays => Dollars.Of(4),
            LicensingModel.LifeLong => Dollars.Of(8),
            _ => throw new ArgumentOutOfRangeException()
        };

        if (status.IsAdvance) price = price * 0.75m;

        return price;
    }

    public void PurchaseMovie(Customer customer, Movie movie)
    {
        var expirationDate = _movieService.GetExpirationDate(movie.LicensingModel);
        var price = CalculatePrice(customer.Status, movie.LicensingModel);

        customer.AddPurchasedMovie(movie, expirationDate, price);
    }

    public bool PromoteCustomer(Customer customer = default)
    {
        // at least 2 active movies during the last 30 days
        if (customer.PurchasedMovies.Count(x =>
                x.ExpirationDate == ExpirationDate.Infinite ||
                x.ExpirationDate >= DateTime.UtcNow.AddDays(-30)) < 2)
            return false;

        // at least 100 dollars spent during the last year
        if (customer.PurchasedMovies.Where(x => x.PurchaseDate > DateTime.UtcNow.AddYears(-1))
                .Sum(x => x.Price?.Value) < 100m)
            return false;

        customer.Status = customer.Status.Promote();
        return true;
    }
}