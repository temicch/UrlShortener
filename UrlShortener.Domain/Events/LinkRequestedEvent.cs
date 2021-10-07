using System;
using UrlShortener.Domain.Common;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Domain.Events
{
    public record LinkRequestedEvent : DomainEvent<ShortLink>
    {
        public LinkRequestedEvent(ShortLink Payload, DateTime? CreatedAt = null) : base(Payload, CreatedAt)
        {
        }
    }
}