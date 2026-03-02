using Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, 
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(nameof(BotDbContext));

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string is not set");
        }

        services.AddDbContext<BotDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        
        return services;

        // services.AddScoped<ISentNewsRepository>();
            
            // return services;
    }

    
}