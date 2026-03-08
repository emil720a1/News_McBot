namespace Domain.Entity;

public class SentNews
{
    public long Id { get; set; }
    
    public string ArticleUrlHash { get; set; }
    public DateTime SentAt { get; set; }
    
    
    public long UserId { get; set; }
    public User User { get; set; } = null!;


}