using Application.Repositories;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SubscriptionRepository : ISubscriptionRepository
{
    
    private readonly BotDbContext _dbContext;

    public SubscriptionRepository(BotDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddAsync(Subscriptions subscription, CancellationToken cancellationToken)
    {
        _dbContext.Subscriptions.AddAsync(subscription, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(Subscriptions subscription, CancellationToken cancellationToken)
    {
        _dbContext.Subscriptions.Remove(subscription);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> IsSubscribedAsync(long userId, string topic, CancellationToken cancellationToken)
    {
        return await _dbContext.Subscriptions
            .AnyAsync(s => s.UserId == userId && s.Topic == topic, cancellationToken);
    }

    public async Task<IEnumerable<Subscriptions>> GetByUserIdAsync(long userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Subscriptions
            .Where(s => s.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Subscriptions?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _dbContext.Subscriptions
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }
}