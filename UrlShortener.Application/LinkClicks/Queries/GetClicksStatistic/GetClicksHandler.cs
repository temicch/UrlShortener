using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using UrlShortener.Application.Interfaces;
using UrlShortener.Application.Interfaces.Paginated;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Implementation.LinkClicks.Queries.GetClicksStatistic
{
    public class GetClicksHandler : IPaginatedRequestHandler<GetClicksRequest, GetClicksResponse>
    {
        private readonly IConfigurationProvider _configurationProvider;
        private readonly IDbContext _dbContext;

        public GetClicksHandler(IDbContext dbContext, IConfigurationProvider configurationProvider)
        {
            _dbContext = dbContext;
            _configurationProvider = configurationProvider;
        }

        public async Task<PaginatedList<GetClicksResponse>> Handle(GetClicksRequest request,
            CancellationToken cancellationToken)
        {
            return await _dbContext.LinkClicks
                .GroupBy(x => new ShortLink(x.Link.Id, x.Link.Link, x.Link.Alias, x.Link.CreatedAt))
                .OrderByDescending(x => x.Count())
                .ProjectTo<GetClicksResponse>(_configurationProvider)
                .ToPaginatedListAsync(request.PageIndex, request.PageSize, cancellationToken);
        }
    }
}