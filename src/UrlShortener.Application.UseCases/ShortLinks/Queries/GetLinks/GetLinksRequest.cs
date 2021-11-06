using UrlShortener.Application.Interfaces.Paginated;

namespace UrlShortener.Application.UseCases.ShortLinks.Queries.GetLinks
{
    /// <summary>
    ///     Represents request for receive <see cref="PaginatedList{T}" />
    ///     of <see cref="GetLinksResponse" />'s
    /// </summary>
    public record GetLinksRequest : PaginatedRequest<GetLinksResponse>
    {
    }
}