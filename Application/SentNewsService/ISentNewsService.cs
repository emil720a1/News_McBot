using Application.DTO_s;
using CSharpFunctionalExtensions;
using Shared;

namespace Application.SentNewsService;

public interface ISentNewsService
{
    Task<Result<IEnumerable<SentNewsDetailsDto>>> GetTopHeadlinesAsync(Guid country, CancellationToken cancellationToken);

    Task<Result> MarkAsSentAsync(Guid userId, string articleUrl, CancellationToken cancellationToken);
}