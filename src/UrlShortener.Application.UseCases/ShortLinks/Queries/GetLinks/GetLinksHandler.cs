﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using UrlShortener.Application.Interfaces;
using UrlShortener.Application.Interfaces.Paginated;

namespace UrlShortener.Application.UseCases.ShortLinks.Queries.GetLinks;

public class GetLinksHandler : IPaginatedRequestHandler<GetLinksRequest, GetLinksResponse>
{
    private readonly IConfigurationProvider _configurationProvider;
    private readonly IDbContext _dbContext;

    public GetLinksHandler(IDbContext dbContext, IConfigurationProvider configurationProvider)
    {
        _dbContext = dbContext;
        _configurationProvider = configurationProvider;
    }

    public Task<PaginatedList<GetLinksResponse>> Handle(GetLinksRequest request,
        CancellationToken cancellationToken)
    {
        return _dbContext.ShortLinks
            .OrderBy(x => x.CreatedAt)
            .ProjectTo<GetLinksResponse>(_configurationProvider)
            .ToPaginatedListAsync(request.PageIndex, request.PageSize, cancellationToken);
    }
}