using MediatR;
using UrlShortener.Domain.Common;

namespace UrlShortener.Application.Interfaces
{
    /// <summary>
    ///     <see cref="INotification" /> object for <see cref="NotificationHandler{TNotification}" />
    /// </summary>
    /// <typeparam name="TEventPayload">Event payload object</typeparam>
    public class DomainEventNotification<TEventPayload> : INotification
    {
        /// <summary>
        ///     Create notification with specified <see cref="DomainEvent{TPayload}" />
        /// </summary>
        /// <param name="event">Event</param>
        public DomainEventNotification(DomainEvent<TEventPayload> @event)
        {
            Event = @event;
        }

        public DomainEvent<TEventPayload> Event { get; }
    }
}