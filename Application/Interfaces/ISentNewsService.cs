using Application.DTO_s;
using CSharpFunctionalExtensions;
using Shared;

namespace Application.SentNewsService;

public interface ISentNewsService
{
    Task<Result<IEnumerable<SentNewsDetailsDto>>> GetTopHeadlinesAsync(Guid country, CancellationToken cancellationToken);

    Task<Result> MarkAsSentAsync(Guid userId, string articleUrl, CancellationToken cancellationToken);

    Task<Result> RemoveSentAsync(Guid userId, string articleUrl, CancellationToken cancellationToken);
    
    Task<Result<IEnumerable<SentNewsDetailsDto>>> GetNewsByCategoryAsync(Guid userId, string category, CancellationToken cancellationToken);
    Task<bool> IsAlreadySentAsync(Guid userId, string articleUrl, CancellationToken cancellationToken);
}