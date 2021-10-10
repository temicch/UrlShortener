using FluentValidation;
using UrlShortener.Application.Interfaces.Extensions;
using UrlShortener.Application.Interfaces.Services;

namespace UrlShortener.Application.UseCases.ShortLinks.Commands.CreateLink
{
    public class CreateLinkValidator : AbstractValidator<CreateLinkRequest>
    {
        public CreateLinkValidator(IUrlShortenerService urlShortenerService)
        {
            Transform(x => x.Link, y => y?.Trim())
                .CorrectUrl(urlShortenerService);

            When(x => !string.IsNullOrEmpty(x.SuggestedAlias?.Trim()), () =>
            {
                Transform(x => x.SuggestedAlias, y => y?.Trim())
                    .CorrectAlias();
            });
        }
    }
}