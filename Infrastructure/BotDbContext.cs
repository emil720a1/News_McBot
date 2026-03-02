using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class BotDbContext : DbContext
{
    public BotDbContext(DbContextOptions<BotDbContext> options) : base(options)
    { }
    
    public DbSet<User> Users { get; set; }
    
    public DbSet<Subscriptions> Subscriptions { get; set; }
    
    public DbSet<SentNews> SentNews { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BotDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}