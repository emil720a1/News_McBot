using Domain.Entity;

namespace Application.Repositories;

public interface ISubscriptionRepository
{
    Task AddAsync(Subscriptions subscription, CancellationToken cancellationToken);
    
    Task RemoveAsync(Subscriptions subscription, CancellationToken cancellationToken);
    
    Task<bool> IsSubscribedAsync(Guid userId, string topic, CancellationToken cancellationToken);
    
    Task<IEnumerable<Subscriptions>> GetByUserIdAsync (Guid userId, CancellationToken cancellationToken);
    
    Task<Subscriptions?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}