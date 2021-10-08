using CSharpFunctionalExtensions;
using MediatR;

namespace UrlShortener.Application.Implementation.ShortLinks.Commands.CreateLink
{
    /// <summary>
    ///     Represents request for creating <see cref="Domain.Entities.ShortLink" />
    /// </summary>
    public class CreateLinkRequest : IRequest<IResult<CreateLinkResponse>>
    {
        /// <summary>
        ///     <para>
        ///         Create <see cref="Domain.Entities.ShortLink" />
        ///     </para>
        ///     <para>
        ///         Alias will be generated automatically if
        ///         <paramref name="suggestedAlias" /> is <see langword="null" />
        ///     </para>
        /// </summary>
        public CreateLinkRequest(string encodedUrl, string suggestedAlias = null)
        {
            Link = encodedUrl;
            SuggestedAlias = suggestedAlias;
        }

        public CreateLinkRequest()
        {
        }

        public string Link { get; set; }
        public string SuggestedAlias { get; set; }
    }
}