using FluentValidation;
using UrlShortener.Application.Interfaces.Extensions;
using UrlShortener.Application.Interfaces.Services;

namespace UrlShortener.Application.UseCases.ShortLinks.Commands.CreateLink;

public class CreateLinkValidator : AbstractValidator<CreateLinkRequest>
{
    public CreateLinkValidator(IUrlShortenerService urlShortenerService)
    {
        Transform(x => x.EncodedUrl, y => y?.Trim())
            .CorrectUrl(urlShortenerService);

        When(x => !string.IsNullOrWhiteSpace(x.SuggestedAlias), () =>
        {
            Transform(x => x.SuggestedAlias, y => y?.Trim())
                .CorrectAlias();
        });
    }
}