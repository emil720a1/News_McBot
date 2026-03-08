namespace Application.Interfaces;

public interface IBotUserService
{
    Task RegisterUserIfNotExist(long userId, string username, CancellationToken cancellationToken);
}