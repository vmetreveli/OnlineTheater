using OnlineTheater.Domains.Primitives;
using OnlineTheater.Domains.ValueObjects;

namespace OnlineTheater.Domains.Entities;

public sealed class PurchasedMovie : EntityBase
{
    public Movie? Movie { get; private set; }

    public Customer Customer { get; private set; }

    public Dollars Price { get; private set; }

    public DateTime PurchaseDate { get;}

    public ExpirationDate? ExpirationDate { get;  }

    public Guid? MovieId { get; private set; }
    public Guid? CustomerId { get; private set; }

    private PurchasedMovie()
    {

    }
    public PurchasedMovie(Movie? movie, Customer customer, Dollars price,
        ExpirationDate? expirationDate)
    {
        if (price is null || price.IsZero)
        {
            throw new ArgumentException(nameof(price));
        }
        if (expirationDate is null || expirationDate.IsExpired)
        {
            throw new ArgumentException(nameof(expirationDate));
        }
        Movie = movie ?? throw new ArgumentNullException(nameof(movie));
        Customer = customer ?? throw new ArgumentNullException(nameof(customer));
        Price = price;
        ExpirationDate = expirationDate;
        PurchaseDate = DateTime.UtcNow;
    }
}