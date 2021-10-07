using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Application.Interfaces;
using UrlShortener.Application.Interfaces.Services;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Events;

namespace UrlShortener.Application.Implementation.ShortLinks.Queries.GetLink
{
    public class GetLinkHandler : IRequestHandler<GetLinkRequest, IResult<ShortLink>>
    {
        private readonly IDateTimeService _dateTime;
        private readonly IDbContext _dbContext;
        private readonly IDomainEventService _domainEventService;

        public GetLinkHandler(IDbContext dbContext,
            IDomainEventService domainEventService,
            IDateTimeService dateTime)
        {
            _dbContext = dbContext;
            _domainEventService = domainEventService;
            _dateTime = dateTime;
        }


        public async Task<IResult<ShortLink>> Handle(GetLinkRequest request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.ShortLinks
                .Where(x => x.Alias == request.Alias)
                .SingleOrDefaultAsync(cancellationToken);

            if (result == null)
                return Result.Failure<ShortLink>("Unable to get that link. It is not exists yet");

            await _domainEventService.PublishAsync(new LinkRequestedEvent(result, _dateTime.Now),
                cancellationToken);

            return Result.Success(result);
        }
    }
}