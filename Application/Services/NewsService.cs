using System.Net.Http.Json;
using Application.DTO_s.NewsDto_s;
using Application.NewsProviderService;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.NewsProviderService;

public class NewsService : INewsProviderService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    
    public NewsService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["BotConfiguration:NewsApiKey"] ?? string.Empty;
    }
    
    public async Task<Result<IEnumerable<NewsArticleDto>>> GetTopHeadlinesAsync(string country, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(country) || country.Length != 2)
            return Result.Failure<IEnumerable<NewsArticleDto>>("Code has to be 2 characters long");

        try
        {
            var url = $"https://newsapi.org/v2/top-headlines?country={country}&apiKey={_apiKey}";
            var response = await _httpClient.GetFromJsonAsync<NewsApiResponse>(url, cancellationToken);
            
            if (response == null || response.Status != "ok")
                return Result.Failure<IEnumerable<NewsArticleDto>>("Error while fetching news");

            var articles = response.Articles.Select(a => new NewsArticleDto(
                a.Title,
                a.Description ?? "Description is empty",
                a.Url,
                a.UrlToImage,
                a.PublishedAt,
                a.Source.Name
                ));

            return Result.Success(articles);
        }
        catch (Exception e)
        {
            return Result.Failure<IEnumerable<NewsArticleDto>>(e.Message);
        }
    }

    public async Task<Result<IEnumerable<NewsArticleDto>>> GetNewsByCategoryAsync(string category,string country, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(category))
            return Result.Failure<IEnumerable<NewsArticleDto>>("Category is empty");

        if (string.IsNullOrEmpty(country) || country.Length != 2)
            return Result.Failure<IEnumerable<NewsArticleDto>>("Code has to be 2 characters long");

        try
        {
            var url = $"https://newsapi.org/v2/top-headlines?country={country}&category={category}&apiKey={_apiKey}";
            var response = await _httpClient.GetFromJsonAsync<NewsApiResponse>(url, cancellationToken);
            
            if (response == null || response.Status != "ok")
                return Result.Failure<IEnumerable<NewsArticleDto>>("Error while fetching news");
            
            var articles = response.Articles.Select(a => new NewsArticleDto(
                a.Title,
                a.Description ?? "Description is empty",
                a.Url,
                a.UrlToImage,
                a.PublishedAt,
                a.Source.Name
                ));
            
            return Result.Success(articles);
        }
        catch (Exception e)
        {
            return Result.Failure<IEnumerable<NewsArticleDto>>(e.Message);
        }
    }

    public async Task<Result<IEnumerable<NewsArticleDto>>> SearchNewsAsync(string query, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(query))
        {
            return Result.Failure<IEnumerable<NewsArticleDto>>("Query is empty");
        }

        try
        {
            var url = $"https://newsapi.org/v2/everything?q={Uri.EscapeDataString(query)}&apiKey={_apiKey}";
            var response = await _httpClient.GetFromJsonAsync<NewsApiResponse>(url, cancellationToken);
            
            if (response == null || response.Status != "ok")
                return Result.Failure<IEnumerable<NewsArticleDto>>("Error while fetching news");

            var articles = response.Articles.Select(a => new NewsArticleDto(
                a.Title,
                a.Description ?? "Description is empty",
                a.Url,
                a.UrlToImage,
                a.PublishedAt,
                a.Source.Name
            ));
            
            return Result.Success(articles);

        }catch (Exception e)
        {
            return Result.Failure<IEnumerable<NewsArticleDto>>(e.Message);
        }
    }
}



internal record NewsApiResponse(string Status, List<NewsApiArticle> Articles);
internal record NewsApiArticle(string Title, string? Description, string Url, string? UrlToImage, DateTime PublishedAt, NewsApiSource Source);
internal record NewsApiSource(string Name);