namespace UsersAPI.Application.Events;

public interface IEventHandler<in TEvent>
{
    Task HandleAsync(TEvent domainEvent);
}