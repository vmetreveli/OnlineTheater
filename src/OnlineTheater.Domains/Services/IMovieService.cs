using OnlineTheater.Domains.ValueObjects;
using Referendum.Domain.Enums;

namespace OnlineTheater.Domains.Services;

public interface IMovieService
{
    ExpirationDate GetExpirationDate(LicensingModel licensingModel);
}