using FluentValidation;
using UrlShortener.Application.Interfaces;

namespace UrlShortener.Application.Implementation.Common
{
    public static class AbstractValidatorExtensions
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
    }
}