using MediatR;

namespace UrlShortener.Application.Implementation.Common
{
    public abstract class PaginatedRequest<TResponse> : IRequest<PaginatedList<TResponse>>
    {
        public virtual int PageSize { get; set; } = 20;
        public virtual int PageIndex { get; set; } = 0;
    }
}