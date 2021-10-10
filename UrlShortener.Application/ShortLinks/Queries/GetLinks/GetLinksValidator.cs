using FluentValidation;
using UrlShortener.Application.Interfaces.Extensions;

namespace UrlShortener.Application.Implementation.ShortLinks.Queries.GetLinks
{
    public class GetLinksValidator : AbstractValidator<GetLinksRequest>
    {
        public GetLinksValidator()
        {
            this.RuleForPaginatedRequest<GetLinksRequest, GetLinksResponse>();
        }
    }
}