using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UrlShortener.Application.Interfaces;
using UrlShortener.Application.Interfaces.Paginated;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.UseCases.LinkClicks.Queries.GetClicksStatistic;

public class GetClicksHandler : IPaginatedRequestHandler<GetClicksRequest, GetClicksResponse>
{
    private readonly IDbContext _dbContext;
    private IConfigurationProvider _configurationProvider;

    public GetClicksHandler(IDbContext dbContext, IConfigurationProvider configurationProvider)
    {
        _dbContext = dbContext;
        _configurationProvider = configurationProvider;
    }

    public async Task<PaginatedList<GetClicksResponse>> Handle(GetClicksRequest request,
        CancellationToken cancellationToken)
    {
        return await _dbContext.LinkClicks.GroupBy(x => new ShortLink
            {
                Id = x.Link.Id,
                Link = x.Link.Link,
                Alias = x.Link.Alias,
                CreatedAt = x.Link.CreatedAt
            })
            .OrderByDescending(x => x.Count())
            .ProjectTo<GetClicksResponse>(_configurationProvider)
            .ToPaginatedListAsync(request.PageIndex, request.PageSize, cancellationToken);
    }
}
