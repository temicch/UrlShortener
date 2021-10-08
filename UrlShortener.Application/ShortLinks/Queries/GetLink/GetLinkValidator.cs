using FluentValidation;
using UrlShortener.Application.Interfaces.Extensions;

namespace UrlShortener.Application.Implementation.ShortLinks.Queries.GetLink
{
    public class GetLinkValidator : AbstractValidator<GetLinkRequest>
    {
        public GetLinkValidator()
        {
            Transform(x => x.Alias, y => y.Trim())
                .CorrectAlias();
        }
    }
}