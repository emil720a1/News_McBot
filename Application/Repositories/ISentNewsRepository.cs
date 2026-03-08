using Domain.Entity;

namespace Application.Repositories;

public interface ISentNewsRepository
{
    Task AddAsync(SentNews sentNews, CancellationToken cancellationToken);
    
    Task<bool> ExistsAsync(long userId, CancellationToken cancellationToken);
    Task RemoveOldRecordsAsync(DateTime beforeDate, CancellationToken cancellationToken);
}