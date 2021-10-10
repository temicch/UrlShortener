using FluentValidation;
using UrlShortener.Application.Interfaces.Extensions;

namespace UrlShortener.Application.Implementation.LinkClicks.Queries.GetClicksStatistic
{
    public class GetClicksValidator : AbstractValidator<GetClicksRequest>
    {
        public GetClicksValidator()
        {
            this.RuleForPaginatedRequest<GetClicksRequest, GetClicksResponse>();
        }
    }
}