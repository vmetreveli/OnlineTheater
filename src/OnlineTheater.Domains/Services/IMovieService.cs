using OnlineTheater.Domains.Enums;
using OnlineTheater.Domains.ValueObjects;

namespace OnlineTheater.Domains.Services;

public interface IMovieService
{
    ExpirationDate GetExpirationDate(LicensingModel licensingModel);
}