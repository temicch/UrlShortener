using System;

namespace UrlShortener.Application.UseCases.LinkClicks.Queries.GetClicksStatistic;

public class GetClicksResponse
{
    public string LinkId { get; set; }
    public DateTime LinkCreatedAt { get; set; }
    public string Alias { get; set; }
    public string Link { get; set; }
    public int ClickCount { get; set; }
    public DateTime LastClickAt { get; set; }
}
