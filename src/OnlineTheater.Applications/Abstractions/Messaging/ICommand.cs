namespace OnlineTheater.Applications.Abstractions.Messaging;

public interface ICommand : IRequest<ErrorOr<Unit>>
{
}

public interface ICommand<TResponse> : IRequest<ErrorOr<TResponse>>
{
}