using OnlineTheater.Domains.Entities;
using OnlineTheater.Domains.Enums;
using OnlineTheater.Domains.ValueObjects;

namespace OnlineTheater.Domains.Services;

public interface ICustomerService
{
    void PurchaseMovie(Customer customer, Movie movie);
}