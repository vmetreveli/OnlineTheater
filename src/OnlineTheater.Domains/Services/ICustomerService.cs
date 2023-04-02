using OnlineTheater.Domains.Entities;
using OnlineTheater.Domains.Enums;
using OnlineTheater.Domains.ValueObjects;
using Referendum.Domain.Enums;

namespace OnlineTheater.Domains.Services;

public interface ICustomerService
{
    Dollars CalculatePrice(CustomerStatus status, ExpirationDate? statusExpirationDate, LicensingModel licensingModel);
    void PurchaseMovie(Customer customer, Movie movie);
    bool PromoteCustomer(Customer customer);
}