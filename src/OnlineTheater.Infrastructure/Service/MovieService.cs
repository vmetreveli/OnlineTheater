﻿using OnlineTheater.Domains.Services;
using OnlineTheater.Domains.ValueObjects;
using Referendum.Domain.Enums;

namespace OnlineTheater.Infrastructure.Service;

public class MovieService : IMovieService
{
    public ExpirationDate GetExpirationDate(LicensingModel licensingModel)
    {
        ExpirationDate result;

        switch (licensingModel)
        {
            case LicensingModel.TwoDays:
                result = (ExpirationDate)DateTime.UtcNow.AddDays(2);
                break;

            case LicensingModel.LifeLong:
                result = ExpirationDate.Infinite;
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }

        return result;
    }
}