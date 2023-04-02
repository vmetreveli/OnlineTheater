using OnlineTheater.Domains.Entities;
using OnlineTheater.Domains.Enums;
using OnlineTheater.Domains.Services;
using OnlineTheater.Domains.ValueObjects;
using Referendum.Domain.Enums;

namespace OnlineTheater.Infrastructure.Service
{

    public class CustomerService : ICustomerService
    {
        private readonly IMovieService _movieService;

        public CustomerService(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public Dollars CalculatePrice(CustomerStatus status, DateTime? statusExpirationDate, LicensingModel licensingModel)
        {
            Dollars price;
            switch (licensingModel)
            {
                case LicensingModel.TwoDays:
                    price = Dollars.Of(4);
                    break;

                case LicensingModel.LifeLong:
                    price = Dollars.Of(8);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (status == CustomerStatus.Advanced && (statusExpirationDate == null || statusExpirationDate.Value >= DateTime.UtcNow))
            {
                price = price * 0.75m;
            }

            return price;
        }

        public void PurchaseMovie(Customer customer, Movie movie)
        {
            DateTime? expirationDate = _movieService.GetExpirationDate(movie.LicensingModel);
            Dollars price = CalculatePrice(customer.Status, customer.StatusExpirationDate, movie.LicensingModel);

            var purchasedMovie = new PurchasedMovie
            {
                MovieId = movie.Id,
                CustomerId = customer.Id,
                ExpirationDate = expirationDate,
                Price = price
            };

            customer.PurchasedMovies.Add(purchasedMovie);
            customer.MoneySpent += price;
        }

        public bool PromoteCustomer(Customer customer)
        {
            // at least 2 active movies during the last 30 days
            if (customer.PurchasedMovies.Count(x => x.ExpirationDate == null || x.ExpirationDate.Value >= DateTime.UtcNow.AddDays(-30)) < 2)
                return false;

            // at least 100 dollars spent during the last year
            if (customer.PurchasedMovies.Where(x => x.PurchaseDate > DateTime.UtcNow.AddYears(-1))
                    .Sum(x => x.Price.Value) < 100m)
                return false;

            customer.Status = CustomerStatus.Advanced;
            customer.StatusExpirationDate = DateTime.UtcNow.AddYears(1);

            return true;
        }
    }
}
