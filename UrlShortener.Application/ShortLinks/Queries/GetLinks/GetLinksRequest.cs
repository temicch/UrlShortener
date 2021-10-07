using UrlShortener.Application.Implementation.Common;

namespace UrlShortener.Application.Implementation.ShortLinks.Queries.GetLinks
{
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