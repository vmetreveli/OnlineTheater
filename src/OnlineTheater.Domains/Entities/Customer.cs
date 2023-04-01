using OnlineTheater.Domains.Enums;
using OnlineTheater.Domains.Primitives;
using OnlineTheater.Domains.ValueObjects;

namespace OnlineTheater.Domains.Entities;

public sealed class Customer : EntityBase
{
    private string _name;

    public CustomerName Name
    {
        get => CustomerName.Create(_name).Value;
        set => _name = value.Value;
    }

    private string _email;

    public Email Email
    {
        get => Email.Create(_email).Value;
        set => _email = value.Value;
    }


    public CustomerStatus Status { get; set; }
    public DateTime? StatusExpirationDate { get; set; }
    public decimal MoneySpent { get; set; }
    public IList<PurchasedMovie> PurchasedMovies { get; set; }
}