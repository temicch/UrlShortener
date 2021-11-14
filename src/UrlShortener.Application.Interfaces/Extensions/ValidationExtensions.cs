using System.Linq;
using FluentValidation;
using UrlShortener.Application.Interfaces.Common;
using UrlShortener.Application.Interfaces.Paginated;
using UrlShortener.Application.Interfaces.Services;

namespace UrlShortener.Application.Interfaces.Extensions;

public static class ValidationExtensions
{
    /// <summary>
    ///     Default rules for <see cref="PaginatedRequest{TResponse}" />
    /// </summary>
    public static void RuleForPaginatedRequest<TRequest, TResponse>(
        this AbstractValidator<TRequest> validationRules)
        where TRequest : PaginatedRequest<TResponse>
    {
        validationRules.RuleFor(x => x.PageIndex)
            .GreaterThanOrEqualTo(0);

        validationRules.RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(100);
    }

    public static IRuleBuilderOptions<T, string> CorrectAlias<T>(this IRuleBuilderInitial<T, string>
        ruleBuilder)
    {
        return ruleBuilder
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Length(AppConstants.ALIAS_MIN_LENGTH, AppConstants.ALIAS_MAX_LENGTH)
            .Must(x => x.All(x => char.IsLetterOrDigit(x) || x == '_'))
            .WithMessage("Alias must contain only letters, digits or undescores");
    }

    public static IRuleBuilderOptions<T, string> CorrectUrl<T>(this IRuleBuilderInitial<T, string>
        ruleBuilder, IUrlShortenerService urlShortenerService)
    {
        return ruleBuilder
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Must(x => urlShortenerService.IsValidUrl(x))
            .WithMessage("It is not a valid url. Try to specify scheme explicitly");
    }
}
