using CSharpFunctionalExtensions;
using MediatR;

namespace UrlShortener.Application.UseCases.ShortLinks.Commands.CreateLink
{
    /// <summary>
    ///     <para>
    ///         Represents request for creating <see cref="Domain.Entities.ShortLink" />
    ///     </para>
    ///     <para>
    ///         Alias will be generated automatically if
    ///         <paramref name="suggestedAlias" /> is <see langword="null" />
    ///     </para>
    /// </summary>
    /// <param name="SuggestedAlias">
    ///     The custom part of the URL
    ///     must be between 3 and 30 characters long. Custom URLs are case sensitive and can
    ///     only consist of upper and lower case letters, numbers, underscores
    /// </param>
    public record CreateLinkRequest
        (string EncodedUrl, string SuggestedAlias = null) : IRequest<IResult<CreateLinkResponse>>
    {
    }
}