using Domain.Entity;

namespace Application.Repositories;

public interface ISubscriptionRepository
{
    Task AddAsync(Subscriptions subscription, CancellationToken cancellationToken);
    
    Task RemoveAsync(Subscriptions subscription, CancellationToken cancellationToken);
    
    Task<bool> IsSubscribedAsync(long userId, string topic, CancellationToken cancellationToken);
    
    Task<IEnumerable<Subscriptions>> GetByUserIdAsync (long userId, CancellationToken cancellationToken);
    
    Task<Subscriptions?> GetByIdAsync(long id, CancellationToken cancellationToken);
}