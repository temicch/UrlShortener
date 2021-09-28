using MediatR;

namespace UrlShortener.Application.Implementation.ShortLinks.Commands.CreateLink
{
    public class CreateLinkRequest : IRequest<CreateLinkResponse>
    {
        public CreateLinkRequest(string link, string suggestedAlias = null)
        {
            Link = link;
            SuggestedAlias = suggestedAlias;
        }

        public CreateLinkRequest()
        {
        }

        public string Link { get; set; }
        public string SuggestedAlias { get; set; }
    }
}