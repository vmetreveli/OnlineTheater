using OnlineTheater.Domains.Primitives;
using OnlineTheater.Domains.ValueObjects;

namespace OnlineTheater.Domains.Entities;

public sealed class PurchasedMovie : EntityBase
{
    public Guid MovieId { get; set; }

    public Movie? Movie { get; set; }

    public Guid CustomerId { get; set; }

    public Dollars? Price { get; set; }

    public DateTime PurchaseDate { get; set; }

    public ExpirationDate? ExpirationDate { get; set; }
}