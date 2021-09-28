using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UrlShortener.Domain.Common;

namespace UrlShortener.Application.Interfaces
{
    public interface IEventHandler<TEventPayload> : INotificationHandler<DomainEventNotification<TEventPayload>>
    {
        Task INotificationHandler<DomainEventNotification<TEventPayload>>.Handle(
            DomainEventNotification<TEventPayload> notification,
            CancellationToken cancellationToken)
        {
            return Handle(notification.Event, cancellationToken);
        }

        Task Handle(DomainEvent<TEventPayload> notification, CancellationToken cancellationToken = default);
    }
}