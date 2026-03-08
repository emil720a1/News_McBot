using Application.DTO_s;
using Application.DTO_s.SubscriptionDto;
using CSharpFunctionalExtensions;
using Shared;

namespace Application.SubscriptionService;

public interface ISubscriptionService
{
    Task<Result<SubscriptionDetailsDto, Failure>> SubscribeToTopicAsync(SubscribeDto request, CancellationToken cancellationToken);

    Task<Result<bool, Failure>> UnsubscribeFromTopicAsync(UnSubscribeDto request, CancellationToken cancellationToken);

    Task<Result<bool, Failure>> UnSubscribeFromAllAsync(Guid userId, CancellationToken cancellationToken);
    
    Task<Result<IEnumerable<SubscriptionDetailsDto>, Failure>> GetSubscriptionsAsync(GetSubscriptionByIdDto request, CancellationToken cancellationToken);

    Task<Result<bool, Failure>> IsSubscribedAsync(Guid userId, string topic, CancellationToken cancellationToken);
}