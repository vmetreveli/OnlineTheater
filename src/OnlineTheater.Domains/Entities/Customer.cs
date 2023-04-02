using OnlineTheater.Domains.Enums;
using OnlineTheater.Domains.Primitives;
using OnlineTheater.Domains.ValueObjects;

namespace OnlineTheater.Domains.Entities;

public sealed class Customer : EntityBase
{
    private string? _name;
    public  CustomerName Name
    {
        get => (CustomerName)_name;
        set => _name = value;
    }

    private  string? _email;

    public Email Email
    {
        get => (Email)_email;
        set => _email = value;
    }

    public CustomerStatus Status { get; set; }
    public DateTime? StatusExpirationDate { get; set; }
    public decimal MoneySpent { get; set; }
    public IList<PurchasedMovie> PurchasedMovies { get; set; }

    private Customer()
    {
       // _purchasedMovies = new List<PurchasedMovie>();
    }
    public Customer(CustomerName name, Email email) : this()
    {
        _name = name ?? throw new ArgumentNullException(nameof(name));
        _email = email ?? throw new ArgumentNullException(nameof(email));

        MoneySpent =0;
        Status = CustomerStatus.Regular;

        StatusExpirationDate = null;
    }
}