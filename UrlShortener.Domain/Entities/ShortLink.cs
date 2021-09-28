using UrlShortener.Domain.Common;

namespace UrlShortener.Domain.Entities
{
    public class ShortLink : AuditableEntity, IEntity
    {
        public ShortLink()
        {
        }

        public ShortLink(string link, string alias)
        {
            Link = link;
            Alias = alias;
        }

        public string Link { get; set; }
        public string Alias { get; set; }

        public string Id { get; set; }
    }
}