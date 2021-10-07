using FluentValidation;

namespace UrlShortener.Application.Implementation.ShortLinks.Queries.GetLink
{
    public class GetLinkValidator : AbstractValidator<GetLinkRequest>
    {
        public GetLinkValidator()
        {
            Transform(x => x.Alias, y => y.Trim())
                .NotEmpty()
                .Length(3, 30);
        }
    }
}