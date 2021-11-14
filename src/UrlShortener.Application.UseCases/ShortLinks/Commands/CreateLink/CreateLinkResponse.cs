using UrlShortener.Application.Interfaces.Mapping;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.UseCases.ShortLinks.Commands.CreateLink;

public class CreateLinkResponse : IMapFrom<ShortLink>
{
    public string Id { get; set; }
    public string Alias { get; set; }
    public string Link { get; set; }
}
