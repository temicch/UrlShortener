using MediatR;
using UrlShortener.Domain.Common;

namespace UrlShortener.Application.Interfaces
{
    public class DomainEventNotification<TEventPayload> : INotification
    {
        public DomainEventNotification(DomainEvent<TEventPayload> @event)
        {
            Event = @event;
        }

        public DomainEvent<TEventPayload> Event { get; set; }
    }
}