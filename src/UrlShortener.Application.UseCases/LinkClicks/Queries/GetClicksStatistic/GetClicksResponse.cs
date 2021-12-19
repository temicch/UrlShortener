using System;
using System.Linq;
using AutoMapper;
using UrlShortener.Application.Interfaces.Mapping;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.UseCases.LinkClicks.Queries.GetClicksStatistic;

public class GetClicksResponse : IMapFrom<IGrouping<ShortLink, LinkClick>>
{
    public string LinkId { get; set; }
    public DateTime LinkCreatedAt { get; set; }
    public string Alias { get; set; }
    public string Link { get; set; }
    public int ClickCount { get; set; }
    public DateTime LastClickAt { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<IGrouping<ShortLink, LinkClick>, GetClicksResponse>()
            .ForMember(x => x.LinkId, y => y.MapFrom(z => z.Key.Id))
            .ForMember(x => x.LinkCreatedAt, y => y.MapFrom(z => z.Key.CreatedAt))
            .ForMember(x => x.Link, y => y.MapFrom(z => z.Key.Link))
            .ForMember(x => x.Alias, y => y.MapFrom(z => z.Key.Alias))
            .ForMember(x => x.ClickCount, y => y.MapFrom(z => z.Count()))
            .ForMember(x => x.LastClickAt, y => y.MapFrom(z => z.Max(z => z.CreatedAt)));
    }
}