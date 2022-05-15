using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Application.Interfaces.Paginated;

/// <summary>
///     Represent strongle typed <see cref="List{T}" /> with paginated params like page index, page size
///     and other.
/// </summary>
/// <typeparam name="T"></typeparam>
public class PaginatedList<T>
{
    /// <summary>
    ///     Creates a strongly typed <see cref="List{T}" /> with paginated params
    /// </summary>
    /// <param name="items"><see cref="List{T}" /> of elements</param>
    /// <param name="totalCount">The total number of elements in the sequence</param>
    /// <param name="pageIndex">Current page index</param>
    /// <param name="pageSize">Page size</param>
    public PaginatedList(List<T> items, int totalCount, int pageIndex, int pageSize)
    {
        if (totalCount < 0)
            throw new ArgumentOutOfRangeException(nameof(totalCount), "Total count must be non negative value");

        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        PageSize = pageSize;
        TotalCount = totalCount;
        Items = items;
    }

    public List<T> Items { get; }

    /// <summary>
    ///     Current page index
    /// </summary>
    public int PageIndex { get; }

    /// <summary>
    ///     Page size
    /// </summary>
    public int PageSize { get; }

    /// <summary>
    ///     Total number of pages in <see cref="PaginatedList{T}" />
    /// </summary>
    public int TotalPages { get; }

    /// <summary>
    ///     The total number of elements in the sequence
    /// </summary>
    public int TotalCount { get; }

    /// <summary>
    ///     Returns <see langword="true" /> if <see cref="PaginatedList{T}" /> has previous page
    /// </summary>
    public bool HasPreviousPage => PageIndex + 1 > 1;

    /// <summary>
    ///     Returns <see langword="true" /> if <see cref="PaginatedList{T}" /> has next page
    /// </summary>
    public bool HasNextPage => PageIndex + 1 < TotalPages;

    /// <summary>
    ///     Returns <see langword="true" /> if current page exists
    /// </summary>
    public bool IsPageExists => PageIndex < TotalPages;
}

public static class PaginatedListExtensions
{
    /// <summary>
    ///     Asynchronously creates a <see cref="PaginatedList{TSource}" /> from an <see cref="IQueryable{TSource}" /> by
    ///     enumerating it asynchronously.
    /// </summary>
    /// <typeparam name="TSource">
    ///     The type of the elements of <paramref name="source" />.
    /// </typeparam>
    /// <param name="source">
    ///     An <see cref="IQueryable{T}" /> to create a paginated list from.
    /// </param>
    /// <param name="pageIndex">Offset from first element in sequence</param>
    /// <param name="pageSize">Page size</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation.
    ///     The task result contains a <see cref="PaginatedList{TSource}" />
    ///     that contains elements from the input sequence.
    /// </returns>
    public static async Task<PaginatedList<TSource>> ToPaginatedListAsync<TSource>(this IQueryable<TSource> source,
        int pageIndex, int pageSize,
        CancellationToken cancellationToken = default)
    {
        var count = await source.CountAsync(cancellationToken);
        var items = await source.Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedList<TSource>(items, count, pageIndex, pageSize);
    }
}