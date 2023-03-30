using OnlineTheater.Domains.Entities;
using Referendum.Domain.Enums;

namespace OnlineTheater.Domains.Services;

public interface ICustomerService
{
    decimal CalculatePrice(CustomerStatus status, DateTime? statusExpirationDate, LicensingModel licensingModel);
    void PurchaseMovie(Customer customer, Movie movie);
    bool PromoteCustomer(Customer customer);
}