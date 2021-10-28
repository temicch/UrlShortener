using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Application.Interfaces;
using UrlShortener.Application.Interfaces.Services;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Events;

namespace UrlShortener.Application.UseCases.ShortLinks.Commands.CreateLink
{
    public class CreateLinkHandler : IRequestHandler<CreateLinkRequest, IResult<CreateLinkResponse>>
    {
        public const int COUNT_OF_SHORT_LINK_RETRY = 100;

        private readonly IDbContext _dbContext;
        private readonly IDomainEventService _domainEventService;
        private readonly IMapper _mapper;
        private readonly Random _rand = new();
        private readonly IUrlShortenerService _urlShortenerService;

        public CreateLinkHandler(IDbContext dbContext, IUrlShortenerService urlShortenerService,
            IMapper mapper,
            IDomainEventService domainEventService)
        {
            _dbContext = dbContext;
            _urlShortenerService = urlShortenerService;
            _mapper = mapper;
            _domainEventService = domainEventService;
        }

        public async Task<IResult<CreateLinkResponse>> Handle(CreateLinkRequest request,
            CancellationToken cancellationToken)
        {
            string alias;

            _urlShortenerService.TryNormalizeUrl(request.EncodedUrl, out var normalizedUrl);

            if (!string.IsNullOrEmpty(request.SuggestedAlias))
            {
                alias = request.SuggestedAlias;
                if (await _dbContext.ShortLinks.Where(y => y.Alias == alias).AnyAsync(cancellationToken))
                    return Result.Failure<CreateLinkResponse>(
                        "Link with specified alias is exists. Try to specify another one");
            }
            else
            {
                var sameLink = await GetLinkByUrl(normalizedUrl, cancellationToken);

                if (sameLink != null)
                    return Result.Success(_mapper.Map<CreateLinkResponse>(sameLink));

                alias = await GenerateAlias(normalizedUrl, cancellationToken);
            }

            var shortLink = new ShortLink(normalizedUrl, alias);

            _dbContext.ShortLinks.Add(shortLink);

            await _dbContext.SaveChangesAsync(cancellationToken);

            await _domainEventService.PublishAsync(new LinkCreatedEvent(shortLink), cancellationToken);

            return Result.Success(_mapper.Map<CreateLinkResponse>(shortLink));
        }

        private async Task<string> GenerateAlias(string normalizedUrl,
            CancellationToken cancellationToken)
        {
            string salt = null;

            for (var i = 0; i < COUNT_OF_SHORT_LINK_RETRY; i++)
            {
                if (!_urlShortenerService.TryShortUrl(normalizedUrl, out var alias, salt: salt))
                    throw new UriFormatException("Unable to short this url");

                if (!await IsAliasExists(alias, cancellationToken))
                    return alias;
                salt = _rand.Next().ToString();
            }

            throw new Exception("Can't create alias");
        }

        private async Task<ShortLink> GetLinkByUrl(string normalizedUrl,
            CancellationToken cancellationToken)
        {
            return await _dbContext.ShortLinks
                .Where(x => x.Link == normalizedUrl)
                .FirstOrDefaultAsync(cancellationToken);
        }

        private async Task<bool> IsAliasExists(string alias, CancellationToken cancellationToken)
        {
            return await _dbContext.ShortLinks
                .Where(x => x.Alias == alias)
                .AnyAsync(cancellationToken);
        }
    }
}