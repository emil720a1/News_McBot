using Domain.Entity;

namespace Application.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken);
    
    Task UpdateAsync(User user, CancellationToken cancellationToken);
    
    Task RemoveAsync(User user, CancellationToken cancellationToken);
   
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
    
    Task<User?> GetByTelegramIdAsync(Guid telegramId, CancellationToken cancellationToken);
    
    Task<User> GetAsync(Guid userId, CancellationToken cancellationToken);
    
}