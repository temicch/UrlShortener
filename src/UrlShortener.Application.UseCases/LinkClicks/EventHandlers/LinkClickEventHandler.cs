using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using UrlShortener.Application.Interfaces;
using UrlShortener.Application.Interfaces.Events;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Events;

namespace UrlShortener.Application.UseCases.LinkClicks.EventHandlers
{
    /// <summary>
    ///     <para>
    ///         Handler for <see cref="LinkRequestedEvent" />
    ///     </para>
    ///     <para>
    ///         Fixes a request as a click on a <see cref="ShortLink" />
    ///     </para>
    /// </summary>
    public class LinkClickEventHandler : IEventHandler<LinkRequestedEvent>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public LinkClickEventHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task Handle(LinkRequestedEvent @event, CancellationToken cancellationToken)
        {
            var mapped = _mapper.Map<LinkClick>(@event);

            await _dbContext.LinkClicks.AddAsync(mapped);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}