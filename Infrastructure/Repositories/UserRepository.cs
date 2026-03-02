using Application.Repositories;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly BotDbContext _dbContext;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(BotDbContext dbContext, ILogger<UserRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    
    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        try
        {
             _dbContext.Users.Add(user);
             await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while adding user");
            throw;
        }
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken)
    {
        _dbContext.Users.Update(user);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(User user, CancellationToken cancellationToken)
    {
        _dbContext.Users.Remove(user);
         await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        var user = _dbContext.Users.FirstOrDefaultAsync(s => s.Username == username, cancellationToken);
        
        return await user;
    }

    public async Task<User?> GetByTelegramIdAsync(Guid telegramId, CancellationToken cancellationToken)
    {
        var user = _dbContext.Users.FirstOrDefaultAsync(s => s.TelegramId == telegramId, cancellationToken);

        return await user;
    }

    public async Task<User> GetAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = _dbContext.Users.FirstOrDefaultAsync(s => s.Id == userId, cancellationToken);
        return await user;
    }
}