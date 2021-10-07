using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Application.Interfaces
{
    public class PaginatedList<T>
    {
        public PaginatedList(List<T> items, int totalCount, int pageIndex, int pageSize)
        {
            if (totalCount < 0)
                throw new ArgumentOutOfRangeException(nameof(totalCount), "Total count must be non negative value");

            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            TotalCount = totalCount;
            Items = items;
        }

        public List<T> Items { get; }
        public int PageIndex { get; }
        public int TotalPages { get; }
        public int TotalCount { get; }

        public bool HasPreviousPage => PageIndex + 1 > 1;
        public bool HasNextPage => PageIndex + 1 < TotalPages;
        public bool IsPageExists => PageIndex < TotalPages;
    }

    public static class PaginatedListExtensions
    {
        public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> source,
            int pageIndex, int pageSize,
            CancellationToken cancellationToken = default)
        {
            var count = await source.CountAsync(cancellationToken);
            var items = await source.Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}