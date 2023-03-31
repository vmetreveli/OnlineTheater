using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using OnlineTheater.Domains.Enums;
using OnlineTheater.Domains.Primitives;
using Referendum.Domain.Enums;
using Referendum.Domain.Errors;

namespace OnlineTheater.Domains.Entities;

public sealed class Customer : EntityBase
{
    public string Name { get; set; }
    public string Email { get; set; }
    public CustomerStatus Status { get; set; }
    public DateTime? StatusExpirationDate { get; set; }
    public decimal MoneySpent { get; set; }
    public IList<PurchasedMovie> PurchasedMovies { get; set; }
}