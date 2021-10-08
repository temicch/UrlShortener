using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Interfaces
{
    /// <summary>
    ///     Represents <see cref="DbContext" /> of application
    /// </summary>
    public interface IDbContext : IDisposable, IAsyncDisposable
    {
        /// <summary>
        ///     <inheritdoc cref="DbSet&lt;ShortLink&rt;" />
        /// </summary>
        DbSet<ShortLink> ShortLinks { get; set; }

        /// <summary>
        ///     <inheritdoc cref="DbSet{TEntity}" />
        /// </summary>
        DbSet<LinkClick> LinkClicks { get; set; }

        /// <summary>
        ///     <inheritdoc cref="DbContext.SaveChangesAsync(CancellationToken)" />
        /// </summary>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}