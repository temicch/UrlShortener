using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UrlShortener.Application.Interfaces.Events;
using UrlShortener.Domain.Events;

namespace UrlShortener.Application.UseCases.ShortLinks.EventHandlers
{
    public class LinkCreatedEventHandler : IEventHandler<LinkCreatedEvent>
    {
        private readonly ILogger<LinkCreatedEventHandler> _logger;

        public LinkCreatedEventHandler(ILogger<LinkCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(LinkCreatedEvent @event, CancellationToken cancellationToken = default)
        {
            if (@event.Payload != null)
                _logger.LogInformation("Alias {Alias} created for link {Link}", @event.Payload.Alias,
                    @event.Payload.Link);
            else
                _logger.LogWarning("Null link provided at {Time}", @event.CreatedAt);

            return Task.CompletedTask;
        }
    }
}