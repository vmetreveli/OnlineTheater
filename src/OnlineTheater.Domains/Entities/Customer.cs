using OnlineTheater.Domains.Enums;
using OnlineTheater.Domains.Primitives;
using OnlineTheater.Domains.ValueObjects;

namespace OnlineTheater.Domains.Entities;

public sealed class Customer : EntityBase
{
    public CustomerName Name { get; private set; }

    public Email Email { get; private set; }

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
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Email = email ?? throw new ArgumentNullException(nameof(email));

        MoneySpent = 0;
        Status = CustomerStatus.Regular;

        StatusExpirationDate = null;
    }

    public void UpdateCustomer(CustomerName name)
    {
        Name = name;
    }
}