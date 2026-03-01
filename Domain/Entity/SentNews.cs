namespace Domain.Entity;

public class SentNews
{
    public Guid Id { get; set; }
    
    public string ArticleUrlHash { get; set; }
    public DateTime SentAt { get; set; }
    
    
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;


}