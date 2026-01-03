using Microsoft.Extensions.Logging;
using UsersAPI.Domain.Events;

namespace UsersAPI.Application.Events;

public sealed class UserCreatedEventHandler
    : IEventHandler<UserCreatedEvent>
{
    private readonly ILogger<UserCreatedEventHandler> _logger;

    public UserCreatedEventHandler(
        ILogger<UserCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(UserCreatedEvent domainEvent)
    {
        _logger.LogInformation(
            "User created | Id: {UserId} | Email: {Email} | At: {CreatedAt}",
            domainEvent.UserId,
            domainEvent.Email,
            domainEvent.CreatedAt
        );

        return Task.CompletedTask;
    }
}
