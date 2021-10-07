using System;
using MediatR;
using UrlShortener.Domain.Common;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Events;

namespace UrlShortener.Application.Implementation.ShortLinks.Queries.GetLink
{
    //public class GetLinkRequest : IRequest<IResult<ShortLink>>
    public class GetLinkRequest: IRequest<ShortLink>
    {
        public GetLinkRequest(string alias)
        {
            Alias = alias;
        }

        public string Alias { get; set; }
    }
}