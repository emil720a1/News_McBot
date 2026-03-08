namespace Domain.Entity;

public class Subscriptions
{
    public long Id { get; set; }
    
    public long UserId { get; set; }
    public User User { get; set; } = null!;
    
    
    public string Topic { get; set; }
    
    public bool IsActive { get; set; }
    
}