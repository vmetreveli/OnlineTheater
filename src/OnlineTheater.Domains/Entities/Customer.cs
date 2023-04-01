using OnlineTheater.Domains.Enums;
using OnlineTheater.Domains.Primitives;
using OnlineTheater.Domains.ValueObjects;

namespace OnlineTheater.Domains.Entities;

public sealed class Customer : EntityBase
{
    public CustomerName Name { get; set; }
    public Email Email { get; set; }
    public CustomerStatus Status { get; set; }
    public DateTime? StatusExpirationDate { get; set; }
    public decimal MoneySpent { get; set; }
    public IList<PurchasedMovie> PurchasedMovies { get; set; }
}