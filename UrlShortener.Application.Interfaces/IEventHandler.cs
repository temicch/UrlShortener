using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UrlShortener.Domain.Common;

namespace UrlShortener.Application.Interfaces
{
    /// <summary>
    ///     Event handler for <see cref="DomainEvent{TPayload}" />
    /// </summary>
    /// <typeparam name="TEventPayload">Event payload</typeparam>
    public interface IEventHandler<TEventPayload> : INotificationHandler<DomainEventNotification<TEventPayload>>
    {
        /// <summary>
        ///     This method used for mediator. It is recommended not to override it
        /// </summary>
        /// <param name="notification">Notification</param>
        /// <param name="cancellationToken">
        ///     <see cref="CancellationToken" />
        /// </param>
        Task INotificationHandler<DomainEventNotification<TEventPayload>>.Handle(
            DomainEventNotification<TEventPayload> notification,
            CancellationToken cancellationToken)
        {
            return Handle(notification.Event, cancellationToken);
        }

        /// <summary>
        ///     Handles a notification
        /// </summary>
        /// <param name="notification">Notification</param>
        /// <param name="cancellationToken">
        ///     <see cref="CancellationToken" />
        /// </param>
        Task Handle(DomainEvent<TEventPayload> notification, CancellationToken cancellationToken = default);
    }
}