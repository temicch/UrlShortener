using UrlShortener.Application.Implementation.Common;

namespace UrlShortener.Application.Implementation.LinkClicks.Queries.GetClicksStatistic
{
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