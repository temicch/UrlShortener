using UrlShortener.Application.Interfaces;

namespace UrlShortener.Application.Implementation.ShortLinks.Queries.GetLinks
{
    /// <summary>
    ///     Represents request for receive <see cref="Interfaces.PaginatedList{T}" />
    ///     of <see cref="GetLinksResponse" />'s
    /// </summary>
    public class GetLinksRequest : PaginatedRequest<GetLinksResponse>
    {
        public GetLinksRequest(int pageIndex = 0, int pageSize = 20) : base(pageIndex, pageSize)
        {
        }

        public GetLinksRequest()
        {
        }
    }
}