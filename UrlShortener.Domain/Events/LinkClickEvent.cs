using System;
using UrlShortener.Domain.Common;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Domain.Events
{
    public record LinkClickEvent : DomainEvent<ShortLink>
    {
        public LinkClickEvent(ShortLink Payload, DateTime? CreatedAt = null) : base(Payload, CreatedAt)
        {
        }
    }
}