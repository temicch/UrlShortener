using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UrlShortener.Domain.Common;

namespace UrlShortener.Application.Interfaces.Events
{
    /// <summary>
    ///     Event handler for <see cref="DomainEvent" />
    /// </summary>
    /// <typeparam name="TEvent">Event</typeparam>
    public interface IEventHandler<TEvent> : INotificationHandler<DomainEventNotification<TEvent>>
        where TEvent : DomainEvent
    {
        /// <summary>
        ///     This method used for mediator. It is recommended not to override it
        /// </summary>
        /// <param name="notification">Notification</param>
        /// <param name="cancellationToken">
        ///     <see cref="CancellationToken" />
        /// </param>
        Task INotificationHandler<DomainEventNotification<TEvent>>
            .Handle(DomainEventNotification<TEvent> notification, CancellationToken cancellationToken)
        {
            return Handle(notification.Event, cancellationToken);
        }

        /// <summary>
        ///     Handles an event
        /// </summary>
        /// <param name="event">Event</param>
        /// <param name="cancellationToken">
        ///     <see cref="CancellationToken" />
        /// </param>
        Task Handle(TEvent @event, CancellationToken cancellationToken = default);
    }
}