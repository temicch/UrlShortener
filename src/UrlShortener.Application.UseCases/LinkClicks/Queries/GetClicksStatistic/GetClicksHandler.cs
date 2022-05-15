using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using UrlShortener.Application.Interfaces;
using UrlShortener.Application.Interfaces.Paginated;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.UseCases.LinkClicks.Queries.GetClicksStatistic;

public class GetClicksHandler : IPaginatedRequestHandler<GetClicksRequest, GetClicksResponse>
{
    private readonly IConfigurationProvider _configurationProvider;
    private readonly IDbContext _dbContext;

    public GetClicksHandler(IDbContext dbContext, IConfigurationProvider configurationProvider)
    {
        _dbContext = dbContext;
        _configurationProvider = configurationProvider;
    }

    public Task<PaginatedList<GetClicksResponse>> Handle(GetClicksRequest request,
        CancellationToken cancellationToken)
    {
        return _dbContext.LinkClicks.GroupBy(x => new ShortLink
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