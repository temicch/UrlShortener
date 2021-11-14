using System;
using UrlShortener.Domain.Common;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Domain.Events;

public record LinkRequestedEvent : DomainEvent
{
    public LinkRequestedEvent(ShortLink payload, DateTime? CreatedAt = null) : base(CreatedAt)
    {
        Payload = payload;
    }

    public ShortLink Payload { get; }
}
