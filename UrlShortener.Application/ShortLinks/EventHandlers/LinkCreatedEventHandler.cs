﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UrlShortener.Application.Interfaces.Events;
using UrlShortener.Domain.Events;

namespace UrlShortener.Application.Implementation.ShortLinks.EventHandlers
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
                _logger.LogInformation($"Alias '{@event.Payload.Alias}' for link '{@event.Payload.Link}' created");
            else
                _logger.LogWarning($"Null link provided at {@event.CreatedAt}");

            return Task.CompletedTask;
        }
    }
}