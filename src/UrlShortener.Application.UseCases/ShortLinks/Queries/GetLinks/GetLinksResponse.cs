using System;
using UrlShortener.Application.Interfaces.Mapping;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.UseCases.ShortLinks.Queries.GetLinks
{
    public record GetLinksResponse(string Id, DateTime CreatedAt, string Link, string Alias) : IMapFrom<ShortLink>
    {
    }
}