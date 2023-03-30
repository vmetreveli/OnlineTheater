using Referendum.Domain.Enums;

namespace OnlineTheater.Infrastructure.Service;

public interface IMovieService
{
    DateTime? GetExpirationDate(LicensingModel licensingModel);
}