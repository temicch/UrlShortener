using UrlShortener.Domain.Common;

namespace UrlShortener.Domain.Entities
{
    public class LinkClick : AuditableEntity, IEntity
    {
        public string LinkId { get; set; }
        public ShortLink Link { get; set; }
        public string Id { get; set; }
    }
}