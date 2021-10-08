using System.Threading;
using System.Threading.Tasks;
using UrlShortener.Domain.Common;

namespace UrlShortener.Application.Interfaces.Services
{
    /// <summary>
    ///     Represents service for publishing to event handlers
    /// </summary>
    public interface IDomainEventService
    {
        /// <summary>
        ///     Asynchronously send an event to multiple handlers
        /// </summary>
        /// <typeparam name="TEventPayload">Event payload</typeparam>
        /// <param name="domainEvent">Domain event</param>
        /// <param name="cancellationToken">
        ///     <see cref="CancellationToken" />
        /// </param>
        Task PublishAsync<TEventPayload>(DomainEvent<TEventPayload> domainEvent,
            CancellationToken cancellationToken = default);
    }
}