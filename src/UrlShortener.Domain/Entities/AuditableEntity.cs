using System;

namespace UrlShortener.Domain.Entities;

public abstract class AuditableEntity
{
    public DateTime CreatedAt { get; set; }
}