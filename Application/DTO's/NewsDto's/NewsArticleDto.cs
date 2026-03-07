namespace Application.DTO_s.NewsDto_s;

public record NewsArticleDto(
    string Title,
    string Description, 
    string Url, 
    string? ImageUrl,
    DateTime PublishedAt,
    string SourceName);