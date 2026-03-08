using Application.NewsProviderService;
using Application.Repositories;
using Infrastructure.NewsProviderService;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, 
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("BotDbContext");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string is not set");
        }

        services.AddDbContext<BotDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddHttpClient<INewsProviderService, NewsService>(client =>
        {
            client.DefaultRequestHeaders.Add("User-Agent", "NewsBot-App");
        });
        
        return services;

    }

    
}