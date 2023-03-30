using Referendum.Domain.Enums;

namespace OnlineTheater.Infrastructure.Service
{
    public class MovieService : IMovieService
    {
        public DateTime? GetExpirationDate(LicensingModel licensingModel)
        {
            DateTime? result;

            switch (licensingModel)
            {
                case LicensingModel.TwoDays:
                    result = DateTime.UtcNow.AddDays(2);
                    break;

                case LicensingModel.LifeLong:
                    result = null;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return result;
        }
    }
}
