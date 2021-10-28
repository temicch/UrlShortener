using CSharpFunctionalExtensions;
using MediatR;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.UseCases.ShortLinks.Queries.GetLink
{
    /// <summary>
    ///     Represents request for receive <see cref="ShortLink" />
    ///     with specified <see cref="ShortLink.Alias" />
    /// </summary>
    public record GetLinkRequest(string Alias) : IRequest<IResult<GetLinkResponse>>
    {
    }
}