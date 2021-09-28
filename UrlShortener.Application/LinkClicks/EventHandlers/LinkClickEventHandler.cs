using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using UrlShortener.Application.Interfaces;
using UrlShortener.Domain.Common;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Implementation.LinkClicks.EventHandlers
{
    public class LinkClickEventHandler : IEventHandler<ShortLink>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public LinkClickEventHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task Handle(DomainEvent<ShortLink> notification, CancellationToken cancellationToken)
        {
            var mapped = _mapper.Map<LinkClick>(notification);

            await _dbContext.LinkClicks.AddAsync(mapped);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}