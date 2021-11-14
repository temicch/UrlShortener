using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UrlShortener.Application.Interfaces;
using UrlShortener.Application.Interfaces.Paginated;

namespace UrlShortener.Application.UseCases.LinkClicks.Queries.GetClicksStatistic;

public class GetClicksHandler : IPaginatedRequestHandler<GetClicksRequest, GetClicksResponse>
{
    private readonly IDbContext _dbContext;

    public GetClicksHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PaginatedList<GetClicksResponse>> Handle(GetClicksRequest request,
        CancellationToken cancellationToken)
    {
        return await _dbContext.LinkClicks
            .GroupBy(x => new { x.Link.Id, x.Link.Link, x.Link.Alias, x.Link.CreatedAt })
            .OrderByDescending(x => x.Count())
            // Here may be ProjectTo from AutoMapper, but for some reason GroupBy(class) not working well in
            // ef core 6, so configuring mapper was difficult
            .Select(x => new GetClicksResponse()
            {
                LinkId = x.Key.Id,
                LinkCreatedAt = x.Key.CreatedAt,
                Link = x.Key.Link,
                Alias = x.Key.Alias,
                ClickCount = x.Count(),
                LastClickAt = x.Max(z => z.CreatedAt)
            })
            .ToPaginatedListAsync(request.PageIndex, request.PageSize, cancellationToken);
    }
}
