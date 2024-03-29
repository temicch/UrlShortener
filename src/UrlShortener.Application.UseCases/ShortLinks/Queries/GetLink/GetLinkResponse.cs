﻿using System;
using UrlShortener.Application.Interfaces.Mapping;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.UseCases.ShortLinks.Queries.GetLink;

public class GetLinkResponse : IMapFrom<ShortLink>
{
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Link { get; set; }
    public string Alias { get; set; }
}