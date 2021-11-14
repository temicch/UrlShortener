using MediatR;

namespace UrlShortener.Application.Interfaces.Paginated;

/// <summary>
/// Paginated request for <see cref="IRequestHandler{TRequest,TResponse}" />
/// </summary>
/// <typeparam name="TResponse"><see cref="PaginatedList{TResponse}"> type</typeparam>
public abstract class PaginatedRequest<TResponse> : IRequest<PaginatedList<TResponse>>
{
    public virtual int PageIndex { get; set; } = 0;
    public virtual int PageSize { get; set; } = 20;
}
