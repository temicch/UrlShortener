using MediatR;
using UrlShortener.Application.Interfaces;

namespace UrlShortener.Application.Implementation.Common
{
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