namespace OnlineTheater.Domains.Errors;

/// <summary>
///     Contains the domain errors.
/// </summary>
public static class DomainErrors
{
    /// <summary>
    ///     Contains the user errors.
    /// </summary>
    public static class User
    {
        public static Error NotFound =>
            Error.NotFound("User.NotFound", "The user with the specified identifier was not found.");

        public static Error InvalidPermissions => Error.Validation(
            "User.InvalidPermissions",
            "The current user does not have the permissions to perform that operation.");

        public static Error DuplicateEmail =>
            Error.Validation("User.DuplicateEmail", "The specified email is already in use.");

        public static Error CannotChangePassword => Error.Validation(
            "User.CannotChangePassword",
            "The password cannot be changed to the specified password.");
    }

    /// <summary>
    ///     Contains the attendee errors.
    /// </summary>
    public static class Attendee
    {
        public static Error NotFound => Error.NotFound("Attendee.NotFound",
            "The attendee with the specified identifier was not found.");

        public static Error AlreadyProcessed =>
            Error.Validation("Attendee.AlreadyProcessed", "The attendee has already been processed.");
    }

    /// <summary>
    ///     Contains the category errors.
    /// </summary>
    public static class Category
    {
        public static Error NotFound => Error.NotFound("Category.NotFound",
            "The category with the specified identifier was not found.");
    }

    /// <summary>
    ///     Contains the event errors.
    /// </summary>
    public static class Event
    {
        public static Error AlreadyCancelled =>
            Error.Validation("Event.AlreadyCancelled", "The event has already been cancelled.");

        public static Error EventHasPassed => Error.Validation(
            "Event.EventHasPassed",
            "The event has already passed and cannot be modified.");
    }

    /// <summary>
    ///     Contains the group event errors.
    /// </summary>
    public static class GroupEvent
    {
        public static Error NotFound => Error.NotFound(
            "GroupEvent.NotFound",
            "The group event with the specified identifier was not found.");

        public static Error UserNotFound => Error.NotFound(
            "GroupEvent.UserNotFound",
            "The user with the specified identifier was not found.");

        public static Error FriendNotFound => Error.NotFound(
            "GroupEvent.FriendNotFound",
            "The friend with the specified identifier was not found.");

        public static Error InvitationAlreadySent => Error.Validation(
            "GroupEvent.InvitationAlreadySent",
            "The invitation for this event has already been sent to this user.");

        public static Error NotFriends => Error.NotFound(
            "GroupEvent.NotFriends",
            "The specified users are not friend.");

        public static Error DateAndTimeIsInThePast => Error.Validation(
            "GroupEvent.InThePast",
            "The event date and time cannot be in the past.");
    }

    /// <summary>
    ///     Contains the personal event errors.
    /// </summary>
    public static class PersonalEvent
    {
        public static Error NotFound => Error.NotFound(
            "GroupEvent.NotFound",
            "The group event with the specified identifier was not found.");

        public static Error UserNotFound => Error.NotFound(
            "GroupEvent.UserNotFound",
            "The user with the specified identifier was not found.");

        public static Error DateAndTimeIsInThePast => Error.Validation(
            "GroupEvent.InThePast",
            "The event date and time cannot be in the past.");

        public static Error AlreadyProcessed =>
            Error.Validation("PersonalEvent.AlreadyProcessed", "The event has already been processed.");
    }

    /// <summary>
    ///     Contains the notification errors.
    /// </summary>
    public static class Notification
    {
        public static Error AlreadySent =>
            Error.Validation("Notification.AlreadySent", "The notification has already been sent.");
    }

    /// <summary>
    ///     Contains the invitation errors.
    /// </summary>
    public static class Invitation
    {
        public static Error NotFound => Error.NotFound(
            "Invitation.NotFound",
            "The invitation with the specified identifier was not found.");

        public static Error EventNotFound => Error.NotFound(
            "Invitation.EventNotFound",
            "The event with the specified identifier was not found.");

        public static Error UserNotFound => Error.NotFound(
            "Invitation.UserNotFound",
            "The user with the specified identifier was not found.");

        public static Error FriendNotFound => Error.NotFound(
            "Invitation.FriendNotFound",
            "The friend with the specified identifier was not found.");

        public static Error AlreadyAccepted =>
            Error.Validation("Invitation.AlreadyAccepted", "The invitation has already been accepted.");

        public static Error AlreadyRejected =>
            Error.Validation("Invitation.AlreadyRejected", "The invitation has already been rejected.");
    }

    /// <summary>
    ///     Contains the friendship errors.
    /// </summary>
    public static class Friendship
    {
        public static Error UserNotFound => Error.NotFound(
            "Friendship.UserNotFound",
            "The user with the specified identifier was not found.");

        public static Error FriendNotFound => Error.NotFound(
            "Friendship.FriendNotFound",
            "The friend with the specified identifier was not found.");

        public static Error NotFriends => Error.NotFound(
            "Friendship.NotFriends",
            "The specified users are not friend.");
    }

    /// <summary>
    ///     Contains the friendship request errors.
    /// </summary>
    public static class FriendshipRequest
    {
        public static Error NotFound => Error.NotFound(
            "FriendshipRequest.NotFound",
            "The friendship request with the specified identifier was not found.");

        public static Error UserNotFound => Error.NotFound(
            "FriendshipRequest.UserNotFound",
            "The user with the specified identifier was not found.");

        public static Error FriendNotFound => Error.NotFound(
            "FriendshipRequest.FriendNotFound",
            "The friend with the specified identifier was not found.");

        public static Error AlreadyAccepted => Error.Validation(
            "FriendshipRequest.AlreadyAccepted",
            "The friendship request has already been accepted.");

        public static Error AlreadyRejected => Error.Validation(
            "FriendshipRequest.AlreadyRejected",
            "The friendship request has already been rejected.");

        public static Error AlreadyFriends => Error.Validation(
            "FriendshipRequest.AlreadyFriends",
            "The friendship request can not be sent because the users are already friends.");

        public static Error PendingFriendshipRequest => Error.Validation(
            "FriendshipRequest.PendingFriendshipRequest",
            "The friendship request can not be sent because there is a pending friendship request.");
    }

    /// <summary>
    ///     Contains the name errors.
    /// </summary>
    public static class Name
    {
        public static Error NullOrEmpty => Error.Validation("Name.NullOrEmpty", "The name is required.");

        public static Error LongerThanAllowed =>
            Error.Validation("Name.LongerThanAllowed", "The name is longer than allowed.");
    }

    /// <summary>
    ///     Contains the first name errors.
    /// </summary>
    public static class FirstName
    {
        public static Error NullOrEmpty => Error.Validation("FirstName.NullOrEmpty", "The first name is required.");

        public static Error LongerThanAllowed =>
            Error.Validation("FirstName.LongerThanAllowed", "The first name is longer than allowed.");
    }

    /// <summary>
    ///     Contains the last name errors.
    /// </summary>
    public static class LastName
    {
        public static Error NullOrEmpty => Error.Validation("LastName.NullOrEmpty", "The last name is required.");

        public static Error LongerThanAllowed =>
            Error.Validation("LastName.LongerThanAllowed", "The last name is longer than allowed.");
    }

    /// <summary>
    ///     Contains the email errors.
    /// </summary>
    public static class Email
    {
        public static Error NullOrEmpty => Error.Validation("Email.NullOrEmpty", "The email is required.");

        public static Error LongerThanAllowed =>
            Error.Validation("Email.LongerThanAllowed", "The email is longer than allowed.");

        public static Error InvalidFormat => Error.Validation("Email.InvalidFormat", "The email format is invalid.");
    }

    /// <summary>
    ///     Contains the password errors.
    /// </summary>
    public static class Password
    {
        public static Error NullOrEmpty => Error.Validation("Password.NullOrEmpty", "The password is required.");

        public static Error TooShort => Error.Validation("Password.TooShort", "The password is too short.");

        public static Error MissingUppercaseLetter => Error.Validation(
            "Password.MissingUppercaseLetter",
            "The password requires at least one uppercase letter.");

        public static Error MissingLowercaseLetter => Error.Validation(
            "Password.MissingLowercaseLetter",
            "The password requires at least one lowercase letter.");

        public static Error MissingDigit => Error.Validation(
            "Password.MissingDigit",
            "The password requires at least one digit.");

        public static Error MissingNonAlphaNumeric => Error.Validation(
            "Password.MissingNonAlphaNumeric",
            "The password requires at least one non-alphanumeric.");
    }

    /// <summary>
    ///     Contains the authentication errors.
    /// </summary>
    public static class Authentication
    {
        public static Error InvalidEmailOrPassword => Error.Validation(
            "Authentication.InvalidEmailOrPassword",
            "The specified email or password are incorrect.");
    }
}