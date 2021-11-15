using UrlShortener.Domain.Common;

namespace UrlShortener.Domain.Entities;

public class ShortLink : AuditableEntity, IEntity
{
    public string Link { get; set; }
    public string Alias { get; set; }

    public string Id { get; set; }
}
