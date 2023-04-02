﻿namespace Referendum.Domain.Errors;

/// <summary>
///     Contains the domain errors.
/// </summary>
public static class DomainErrors
{
    // public const string UserProfileNotFound = "No UserProfile found with ID {0}";
    public static class UserProfiles
    {
        public static Error NotFound(Guid id) =>
            Error.NotFound("UserProfile.NotFound", $"No UserProfile found with ID = {id}");
    }
}