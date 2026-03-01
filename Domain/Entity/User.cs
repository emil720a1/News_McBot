namespace Domain.Entity;

public class User
{
    public Guid Id { get; set; }
    
    public Guid TelegramId { get; set; }
    
    public string Username { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public string LanguageCode { get; set; }
    
    public List<Subscriptions> Subscriptions { get; set; } = new();
    
    public List<SentNews> SentNews { get; set; } = new();


    public void AddSubscription(string topic)
    {
        if (!Subscriptions.Any(x => x.Topic == topic))
        {
            Subscriptions.Add(new Subscriptions
            {
                Topic = topic,
                UserId = this.Id
            });
        }
    }
}