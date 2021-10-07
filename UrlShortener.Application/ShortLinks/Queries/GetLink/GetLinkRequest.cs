using CSharpFunctionalExtensions;
using MediatR;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Implementation.ShortLinks.Queries.GetLink
{
    public class GetLinkRequest : IRequest<IResult<ShortLink>>
    {
        public GetLinkRequest(string alias)
        {
            Alias = alias;
        }

        public string Alias { get; set; }
    }
}