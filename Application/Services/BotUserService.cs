using Application.Interfaces;
using Application.Repositories;
using Domain.Entity;

namespace Application.Services;

public class BotUserService(IUserRepository userRepository) : IBotUserService
{
    public async Task RegisterUserIfNotExist(long userId, string username, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByTelegramIdAsync(userId, cancellationToken);
        if (user == null)
        {
            await userRepository.AddAsync(new User(userId, username), cancellationToken);
        }
    }
}