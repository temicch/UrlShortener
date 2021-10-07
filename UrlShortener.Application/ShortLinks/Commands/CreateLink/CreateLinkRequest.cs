using CSharpFunctionalExtensions;
using MediatR;

namespace UrlShortener.Application.Implementation.ShortLinks.Commands.CreateLink
{
    public class CreateLinkRequest : IRequest<IResult<CreateLinkResponse>>
    {
        public CreateLinkRequest(string link, string suggestedAlias = null)
        {
            Link = link;
            SuggestedAlias = suggestedAlias;
        }

        public string Link { get; set; }
        public string SuggestedAlias { get; set; }
    }
}