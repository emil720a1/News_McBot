using Application.Repositories;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SentNewsRepository : ISentNewsRepository
{
    private readonly BotDbContext _dbContext;

    public SentNewsRepository(BotDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddAsync(SentNews sentNews, CancellationToken cancellationToken)
    {
        _dbContext.SentNews.AddAsync(sentNews, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(long userId, CancellationToken cancellationToken)
    {
        return await _dbContext.SentNews
            .AnyAsync(s => s.UserId == userId);
    }

    public async Task RemoveOldRecordsAsync(DateTime beforeDate, CancellationToken cancellationToken)
    {
        var oldRecords = _dbContext.SentNews
            .Where(s => s.SentAt < beforeDate);
        
        await oldRecords.ExecuteDeleteAsync(cancellationToken);
    }
}