using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Application.Interfaces;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Implementation.ShortLinks.Queries.GetLink
{
    public class GetLinkHandler : IRequestHandler<GetLinkRequest, ShortLink>
    {
        private readonly IDbContext _dbContext;

        public GetLinkHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ShortLink> Handle(GetLinkRequest request, CancellationToken cancellationToken)
        {
            return await _dbContext.ShortLinks
                .Where(x => x.Alias == request.Alias)
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}