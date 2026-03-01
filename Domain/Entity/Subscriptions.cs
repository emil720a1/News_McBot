namespace Domain.Entity;

public class Subscriptions
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    
    
    public string Topic { get; set; }
    
    public bool IsActive { get; set; }
    
}