using MediatR;

namespace UrlShortener.Application.Interfaces.Paginated;

/// <summary>
///     Defines a handler for <see cref="PaginatedRequest{TResponse}" />
/// </summary>
public interface IPaginatedRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, PaginatedList<TResponse>>
    where TRequest : IRequest<PaginatedList<TResponse>>
{
}