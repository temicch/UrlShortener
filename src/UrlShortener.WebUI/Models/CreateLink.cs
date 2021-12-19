using System.Linq;
using FluentValidation;
using UrlShortener.Application.Interfaces.Common;
using UrlShortener.Application.Interfaces.Services;
using UrlShortener.Application.UseCases.ShortLinks.Commands.CreateLink;

namespace UrlShortener.WebUI.Models;

public class CreateLink
{
    public string Link { get; set; }
    public string SuggestedAlias { get; set; }
    public bool IsAliasUsed { get; set; }

    public CreateLinkRequest ToRequest()
    {
        return new CreateLinkRequest(Link, IsAliasUsed ? SuggestedAlias : string.Empty);
    }
}

public class CreateLinkValidator : AbstractValidator<CreateLink>
{
    public CreateLinkValidator(IUrlShortenerService urlShortenerService)
    {
        Transform(x => x.Link, y => y?.Trim())
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Please enter a link")
            .Must(x => urlShortenerService.IsValidUrl(x))
            .WithMessage("It is not a valid url. Try to specify scheme explicitly");

        When(x => x.IsAliasUsed, () =>
        {
            Transform(x => x.SuggestedAlias, y => y?.Trim())
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Please enter an alias")
                .Length(AppConstants.ALIAS_MIN_LENGTH, AppConstants.ALIAS_MAX_LENGTH)
                .WithMessage("Incorrect length of alias, it must be from {MinLength} to {MaxLength}")
                .Must(x => x.All(x => char.IsLetterOrDigit(x) || x == '_'))
                .WithMessage("Alias must contain only letters, digits or underscores");
        });
    }
}