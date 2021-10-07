using MediatR;

namespace UrlShortener.Application.Interfaces
{
    /// <summary>
    ///     Paginated request for <see cref="IRequestHandler{TRequest,TResponse}" />
    /// </summary>
    /// <typeparam name="TResponse">
    ///     <see cref="PaginatedList{TResponse}"> type
    /// </typeparam>
    public abstract class PaginatedRequest<TResponse> : IRequest<PaginatedList<TResponse>>
    {
        protected PaginatedRequest(int pageIndex = 0, int pageSize = 20)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
        }

        public virtual int PageSize { get; set; } = 20;
        public virtual int PageIndex { get; set; }
    }
}