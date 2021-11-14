using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Application.Interfaces;
using UrlShortener.Application.Interfaces.Services;
using UrlShortener.Domain.Events;

namespace UrlShortener.Application.UseCases.ShortLinks.Queries.GetLink;

public class GetLinkHandler : IRequestHandler<GetLinkRequest, IResult<GetLinkResponse>>
{
    private readonly IDateTimeService _dateTime;
    private readonly IDbContext _dbContext;
    private readonly IDomainEventService _domainEventService;
    private readonly IMapper _mapper;

    public GetLinkHandler(IDbContext dbContext,
        IDomainEventService domainEventService,
        IDateTimeService dateTime,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _domainEventService = domainEventService;
        _dateTime = dateTime;
        _mapper = mapper;
    }


    public async Task<IResult<GetLinkResponse>> Handle(GetLinkRequest request, CancellationToken cancellationToken)
    {
        var result = await _dbContext.ShortLinks
            .AsNoTracking()
            .Where(x => x.Alias == request.Alias)
            .SingleOrDefaultAsync(cancellationToken);

        if (result == null)
            return Result.Failure<GetLinkResponse>("Unable to get that link. It is not exists yet");

        await _domainEventService.PublishAsync(new LinkRequestedEvent(result, _dateTime.Now),
            cancellationToken);

        return Result.Success(_mapper.Map<GetLinkResponse>(result));
    }
}
