using System;

namespace UrlShortener.Domain.Common
{
    public abstract record DomainEvent<TPayload>(TPayload Payload, DateTime? CreatedAt = null);
}