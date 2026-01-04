namespace UsersAPI.Application.Events;

public sealed record UserCreatedIntegrationEvent(
    Guid UserId,
    string Email,
    DateTime OccurredAt
);