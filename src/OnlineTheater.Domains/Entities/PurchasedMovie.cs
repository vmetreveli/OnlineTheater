using System.Text.Json.Serialization;
using OnlineTheater.Domains.Primitives;

namespace OnlineTheater.Domains.Entities;

public sealed class PurchasedMovie : EntityBase
{
    public Guid MovieId { get; set; }

    public Movie Movie { get; set; }

    public Guid CustomerId { get; set; }

    public decimal Price { get; set; }

    public DateTime PurchaseDate { get; set; }

    public DateTime? ExpirationDate { get; set; }
}