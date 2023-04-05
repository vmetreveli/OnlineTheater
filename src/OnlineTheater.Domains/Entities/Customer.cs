using OnlineTheater.Domains.Enums;
using OnlineTheater.Domains.Primitives;
using OnlineTheater.Domains.ValueObjects;

namespace OnlineTheater.Domains.Entities;

public sealed class Customer : EntityBase
{
    private Customer()
        => _purchasedMovies = new List<PurchasedMovie>();

    public Customer(CustomerName name, Email email) : this()
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Email = email ?? throw new ArgumentNullException(nameof(email));

        MoneySpent = Dollars.Of(0);
        Status = CustomerStatus.Regular;
    }

    public CustomerName? Name { get; private set; }

    public Email? Email { get; private set; }

    public CustomerStatus? Status { get; set; }
    public Dollars? MoneySpent { get; private set; }
    private readonly IList<PurchasedMovie> _purchasedMovies;
    public IEnumerable<PurchasedMovie?> PurchasedMovies => _purchasedMovies.ToList();

    public void UpdateCustomer(CustomerName name) => Name = name;

    public void PurchaseMovie(Movie movie)
    {
        var expirationDate = movie.GetExpirationDate();
        var price = movie.CalculatePrice(Status);

        var purchasedMovie = new PurchasedMovie(movie, this, price, expirationDate);

        _purchasedMovies.Add(purchasedMovie);
        MoneySpent += price;
    }
    public bool Promote()
    {
        // at least 2 active movies during the last 30 days
        if (PurchasedMovies.Count(x =>
                x.ExpirationDate == ExpirationDate.Infinite ||
                x.ExpirationDate >= DateTime.UtcNow.AddDays(-30)) < 2)
            return false;

        // at least 100 dollars spent during the last year
        if (PurchasedMovies.Where(x => x.PurchaseDate > DateTime.UtcNow.AddYears(-1))
                .Sum(x => x.Price?.Value) < 100m)
            return false;

        Status = Status.Promote();
        return true;
    }
}