using Application;
using Application.NewsProviderService;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presenters.Workers;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddHostedService<TelegramBotWorker>();

var host = builder.Build();

using (var scope = host.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BotDbContext>();
    dbContext.Database.Migrate();

    var newsService = scope.ServiceProvider.GetRequiredService<INewsProviderService>();
    
    var result = await newsService.GetTopHeadlinesAsync("ua", CancellationToken.None);

    if (result.IsSuccess)
    {
        Console.WriteLine("Останні новини:");
        foreach (var article in result.Value.Take(3))
        {
            Console.WriteLine($"- {article.Title} (Джерело: {article.SourceName})");
        }
    }
    else
    {
        Console.WriteLine($"Помилка API: {result.Error}");
    }
}

await host.RunAsync();