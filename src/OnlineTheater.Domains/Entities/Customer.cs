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

    public void AddPurchasedMovie(Movie movie, ExpirationDate expirationDate, Dollars dollars)
    {
        var purchasedMovie = new PurchasedMovie(movie, this, dollars, expirationDate);

        _purchasedMovies.Add(purchasedMovie);
        MoneySpent += dollars;
    }
}