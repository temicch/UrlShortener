using System.Linq;
using FluentValidation;
using UrlShortener.Application.Interfaces.Common;
using UrlShortener.Application.Interfaces.Services;

namespace UrlShortener.Application.Implementation.ShortLinks.Commands.CreateLink
{
    public class CreateLinkValidator : AbstractValidator<CreateLinkRequest>
    {
        public CreateLinkValidator(IUrlShortenerService urlShortenerService)
        {
            Transform(x => x.Link, y => y?.Trim())
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(x => urlShortenerService.IsValidUrl(x))
                .WithMessage("It is not a valid url. Try to specify scheme explicitly");

            When(x => !string.IsNullOrEmpty(x.SuggestedAlias?.Trim()), () =>
            {
                Transform(x => x.SuggestedAlias, y => y?.Trim())
                    .Cascade(CascadeMode.Stop)
                    .Length(3, 30)
                    .Must(x => x.All(x => char.IsLetterOrDigit(x)))
                    .WithMessage("Alias must contain only letters or digits");
            });
        }
    }
}