using CSharpFunctionalExtensions;
using MediatR;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Implementation.ShortLinks.Queries.GetLink
{
    /// <summary>
    ///     Represents request for receive <see cref="ShortLink" />
    ///     with specified <see cref="ShortLink.Alias" />
    /// </summary>
    public class GetLinkRequest : IRequest<IResult<GetLinkResponse>>
    {
        public GetLinkRequest(string alias)
        {
            Alias = alias;
        }

        public string Alias { get; set; }
    }
}