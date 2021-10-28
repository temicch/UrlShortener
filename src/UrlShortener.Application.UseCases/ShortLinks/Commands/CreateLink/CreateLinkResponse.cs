using UrlShortener.Application.Interfaces.Mapping;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.UseCases.ShortLinks.Commands.CreateLink
{
    public record CreateLinkResponse(string Id, string Alias, string Link) : IMapFrom<ShortLink>
    {
    }
}