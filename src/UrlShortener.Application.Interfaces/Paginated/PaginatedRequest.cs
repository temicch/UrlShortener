using MediatR;

namespace UrlShortener.Application.Interfaces.Paginated
{
    /// <summary>
    ///     Paginated request for <see cref="IRequestHandler{TRequest,TResponse}" />
    /// </summary>
    /// <typeparam name="TResponse">
    ///     <see cref="PaginatedList{TResponse}"> type
    /// </typeparam>
    public abstract record PaginatedRequest<TResponse>(int PageIndex = 0, int PageSize = 20) :
        IRequest<PaginatedList<TResponse>>
    {
        protected PaginatedRequest() : this(0)
        {
        }
    }
}