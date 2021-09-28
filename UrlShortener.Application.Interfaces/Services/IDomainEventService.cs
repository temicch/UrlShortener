using System.Threading;
using System.Threading.Tasks;
using UrlShortener.Domain.Common;

namespace UrlShortener.Application.Interfaces.Services
{
    public interface IDomainEventService
    {
        Task PublishAsync<TEventPayload>(DomainEvent<TEventPayload> domainEvent,
            CancellationToken cancellationToken = default);
    }
}