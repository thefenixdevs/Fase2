using UsersAPI.Application.Abstractions;

namespace UsersAPI.Infrastructure.Messaging
{
    public sealed class DomainEventDispatcher : IDomainEventDispatcher
    {
        public Task DispatchAsync(IEnumerable<object> domainEvents)
        {
            // PASSO 8: não fazemos nada ainda
            // PASSO 9+: handlers reais

            return Task.CompletedTask;
        }
    }
}
