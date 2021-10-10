using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UrlShortener.Application.Interfaces.Events;
using UrlShortener.Application.Interfaces.Services;
using UrlShortener.Domain.Common;

namespace UrlShortener.Application.Implementation.Services
{
    public class DomainEventService : IDomainEventService
    {
        private readonly IPublisher _mediator;

        public DomainEventService(IPublisher mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishAsync<TEvent>(TEvent domainEvent,
            CancellationToken cancellationToken = default) where TEvent : DomainEvent
        {
            await _mediator.Publish(new DomainEventNotification<TEvent>(domainEvent),
                cancellationToken);
        }
    }
}