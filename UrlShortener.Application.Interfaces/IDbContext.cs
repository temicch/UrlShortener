using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Interfaces
{
    public interface IDbContext : IDisposable, IAsyncDisposable
    {
        DbSet<ShortLink> ShortLinks { get; set; }
        DbSet<LinkClick> LinkClicks { get; set; }

        /// <summary>
        ///     <inheritdoc cref="DbContext.SaveChangesAsync(CancellationToken)" />
        /// </summary>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}