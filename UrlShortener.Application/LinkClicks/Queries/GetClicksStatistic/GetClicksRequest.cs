using UrlShortener.Application.Interfaces.Paginated;

namespace UrlShortener.Application.Implementation.LinkClicks.Queries.GetClicksStatistic
{
    /// <summary>
    ///     Represents request for receive information about <see cref="Domain.Entities.LinkClick" />'s
    /// </summary>
    public class GetClicksRequest : PaginatedRequest<GetClicksResponse>
    {
        public GetClicksRequest(int pageIndex = 0, int pageSize = 20) : base(pageIndex, pageSize)
        {
        }

        public GetClicksRequest() : this(0)
        {
        }
    }
}