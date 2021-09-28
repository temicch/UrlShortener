using System.Linq;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Application.Interfaces;
using UrlShortener.Application.Interfaces.Services;

namespace UrlShortener.Application.Implementation.ShortLinks.Queries.GetLink
{
    public class GetLinkValidator : AbstractValidator<GetLinkRequest>
    {
        public GetLinkValidator(IDbContext dbContext, IUrlShortenerService urlShortenerService)
        {
            Transform(x => x.Alias, y => y.Trim())
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(3, 30)
                .MustAsync(async (link, cancellationToken) =>
                {
                    return await dbContext.ShortLinks
                        .Where(x => x.Alias == link)
                        .AnyAsync(cancellationToken);
                })
                .WithMessage("Unable to get that link. It is not exists yet");
        }
    }
}