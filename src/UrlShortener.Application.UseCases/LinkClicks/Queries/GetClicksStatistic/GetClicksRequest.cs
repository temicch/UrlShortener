using UrlShortener.Application.Interfaces.Paginated;

namespace UrlShortener.Application.UseCases.LinkClicks.Queries.GetClicksStatistic;

/// <summary>
///     Represents request for receive information about <see cref="Domain.Entities.LinkClick" />'s
/// </summary>
public class GetClicksRequest : PaginatedRequest<GetClicksResponse>
{
}