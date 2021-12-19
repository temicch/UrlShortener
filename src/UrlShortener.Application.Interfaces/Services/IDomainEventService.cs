using System.Threading;
using System.Threading.Tasks;
using UrlShortener.Domain.Common;

namespace UrlShortener.Application.Interfaces.Services;

/// <summary>
///     Represents service for publishing to event handlers
/// </summary>
public interface IDomainEventService
{
    /// <summary>
    ///     Asynchronously send an event to multiple handlers
    /// </summary>
    /// <typeparam name="TEvent">
    ///     <see cref="DomainEvent" />
    /// </typeparam>
    /// <param name="domainEvent">Domain event</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    Task PublishAsync<TEvent>(TEvent domainEvent,
        CancellationToken cancellationToken = default) where TEvent : DomainEvent;
}