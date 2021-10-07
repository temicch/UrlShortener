using FluentValidation;

namespace UrlShortener.Application.Implementation.Common
{
    public static class AbstractValidatorExtensions
    {
        public static void RuleForPaginatedRequest<TRequest, TValue>(this AbstractValidator<TRequest> validationRules)
            where TRequest : PaginatedRequest<TValue>
        {
            validationRules.RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0);

            validationRules.RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100);
        }
    }
}