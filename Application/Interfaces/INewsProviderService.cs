using Application.DTO_s.NewsDto_s;
using CSharpFunctionalExtensions;

namespace Application.NewsProviderService;

public interface INewsProviderService
{
    Task<Result<IEnumerable<NewsArticleDto>>> GetTopHeadlinesAsync(string country, CancellationToken cancellationToken);
    
    Task<Result<IEnumerable<NewsArticleDto>>> GetNewsByCategoryAsync(string category, string country, CancellationToken cancellationToken);
    
    Task<Result<IEnumerable<NewsArticleDto>>> SearchNewsAsync(string query, CancellationToken cancellationToken);
}