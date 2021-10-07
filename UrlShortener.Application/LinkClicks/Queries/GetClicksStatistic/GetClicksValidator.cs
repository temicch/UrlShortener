using FluentValidation;
using UrlShortener.Application.Implementation.Common;

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