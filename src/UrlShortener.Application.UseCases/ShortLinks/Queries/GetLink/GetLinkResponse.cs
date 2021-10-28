using System;
using UrlShortener.Application.Interfaces.Mapping;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.UseCases.ShortLinks.Queries.GetLink
{
    public record GetLinkResponse(string Id, DateTime CreatedAt, string Link, string Alias) : IMapFrom<ShortLink>
    {
    }
}