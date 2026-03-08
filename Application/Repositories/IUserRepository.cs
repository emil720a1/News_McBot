using Domain.Entity;

namespace Application.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken);
    
    Task UpdateAsync(User user, CancellationToken cancellationToken);
    
    Task RemoveAsync(User user, CancellationToken cancellationToken);
   
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
    
    Task<User?> GetByTelegramIdAsync(long telegramId, CancellationToken cancellationToken);
    
    Task<User> GetByIdAsync(long userId, CancellationToken cancellationToken);
    
}