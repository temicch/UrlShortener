using MediatR;
using UrlShortener.Domain.Common;

namespace UrlShortener.Application.Interfaces.Events
{
    /// <summary>
    ///     <see cref="INotification" /> object for <see cref="NotificationHandler{TNotification}" />
    /// </summary>
    /// <typeparam name="TEvent">Event</typeparam>
    public class DomainEventNotification<TEvent> : INotification
        where TEvent : DomainEvent
    {
        /// <summary>
        ///     Create notification with specified <see cref="DomainEvent" />
        /// </summary>
        /// <param name="event">Event</param>
        public DomainEventNotification(TEvent @event)
        {
            Event = @event;
        }

        public TEvent Event { get; }
    }
}