using Application.Interfaces;
using Application.NewsProviderService;
using Application.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Presenters.Workers;

public class TelegramBotWorker : BackgroundService
{
    private readonly ILogger<TelegramBotWorker> _logger;
    private readonly ITelegramBotClient _botClient;
    private readonly INewsProviderService _newsService;
    private readonly IServiceScopeFactory _scopeFactory;
    public TelegramBotWorker(
        ILogger<TelegramBotWorker> logger,
        IConfiguration configuration, 
        INewsProviderService newsService, 
        IServiceScopeFactory scopeFactory) 
    {
        _logger = logger;
        _newsService = newsService;
        _scopeFactory = scopeFactory;

        var token = configuration["BotConfiguration:Token"]
                    ?? throw new ArgumentException("BotToken is missing in appsettings.json");
        
        _botClient = new TelegramBotClient(token);
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Бот запускається...");

        var receiveOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };

        _botClient.StartReceiving(
            updateHandler: HandleUpdateAsync,
            errorHandler: HandlePollingErrorAsync,
            receiverOptions: receiveOptions,
            cancellationToken: stoppingToken
            );
        
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    private async Task HandleUpdateAsync(
        ITelegramBotClient botClient, 
        Update update,
        CancellationToken ct)
    {
        if (update.Message is not{ Text: { } messageText } message)
            return;

        var chatId = message.Chat.Id;
        _logger.LogInformation("Повідомлення від {ChatId}: {Text}", chatId, messageText);

        switch (messageText.ToLower())
        {
            case "/start":
                using (var scope = _scopeFactory.CreateScope())
                {
                    var botUserService = scope.ServiceProvider.GetRequiredService<IBotUserService>();
                    
                    var username = message.From?.Username ?? "Unknown";

                    await botUserService.RegisterUserIfNotExist(chatId, username, ct);
                }
                
                await _botClient.SendMessage(chatId, "Привіт!", cancellationToken: ct);
                break;
                
            case "/news":
                await botClient.SendMessage(chatId, "Шукаю найцікавіше для тебе...", cancellationToken: ct);
                
                var result = await _newsService.GetTopHeadlinesAsync("ua", ct);

                if (result.IsSuccess)
                {
                    foreach (var article in result.Value.Take(3))
                    {
                        var text = $"<b>{article.Title}</b>\n\n{article.Description}\n\n<a href='{article.Url}'>Читати далі...</a>";
                        await botClient.SendMessage(chatId, text, parseMode: ParseMode.Html, cancellationToken: ct);
                    }
                }
                else
                {
                    await botClient.SendMessage(chatId, $"Сталася помилка: {result.Error}", cancellationToken: ct);
                }
                break;
            default:
                await botClient.SendMessage(chatId,"Я розумію поки тільки /start та /news", cancellationToken: ct);
                break;
        }
    }

    private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken ct)
    {
        _logger.LogError(exception, "Помилка Telegram API");
        return Task.CompletedTask;
    }
}