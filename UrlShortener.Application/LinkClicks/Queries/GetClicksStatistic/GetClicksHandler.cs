﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using UrlShortener.Application.Implementation.Common;
using UrlShortener.Application.Interfaces;
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
                //.GroupBy(x => new LinkAlias(x.Link.Link, x.Link.Alias, x.Link.Id, x.Link.CreatedAt))
                .GroupBy(x => new ShortLink
                {
                    Link = x.Link.Link,
                    Alias = x.Link.Alias,
                    Id = x.Link.Id,
                    CreatedAt = x.Link.CreatedAt
                })
                .OrderByDescending(x => x.Count())
                .ProjectTo<GetClicksResponse>(_configurationProvider)
                .ToPaginatedListAsync(request.PageIndex, request.PageSize, cancellationToken);
        }
    }
}