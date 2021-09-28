using System;
using MediatR;
using UrlShortener.Application.Interfaces;
using UrlShortener.Domain.Common;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Events;

namespace UrlShortener.Application.Implementation.ShortLinks.Queries.GetLink
{
    public class GetLinkRequest : IRequest<ShortLink>, INotify<ShortLink>
    {
        public GetLinkRequest(string alias)
        {
            Alias = alias;
        }

        public string Alias { get; set; }

        public Func<ShortLink, DateTime?, DomainEvent<ShortLink>> Event =>
            (response, time) => new LinkClickEvent(response, time);
    }
}