using System;

namespace UrlShortener.Domain.Common;

public abstract record DomainEvent(DateTime? CreatedAt = null);