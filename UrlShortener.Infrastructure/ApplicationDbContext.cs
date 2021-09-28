using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Application.Interfaces;
using UrlShortener.Application.Interfaces.Services;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Infrastructure
{
    public class ApplicationDbContext : DbContext, IDbContext
    {
        private readonly IDateTimeService _dateTime;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions,
            IDateTimeService dateTime)
            : base(dbContextOptions)
        {
            _dateTime = dateTime;
        }

        public DbSet<ShortLink> ShortLinks { get; set; }
        public DbSet<LinkClick> LinkClicks { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>().Where(x => x.State == EntityState.Added))
                entry.Entity.CreatedAt = entry.Entity.CreatedAt == default ? _dateTime.Now : entry.Entity.CreatedAt;

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}