﻿using CSharpFunctionalExtensions;
using MediatR;

namespace UrlShortener.Application.UseCases.ShortLinks.Commands.CreateLink;

/// <summary>
///     <para>
///         Represents request for creating <see cref="Domain.Entities.ShortLink" />
///     </para>
///     <para>
///         Alias will be generated automatically if
///         <paramref name="suggestedAlias" /> is <see langword="null" />
///     </para>
/// </summary>
public class CreateLinkRequest : IRequest<IResult<CreateLinkResponse>>
{
    public CreateLinkRequest(string encodedUrl, string suggestedAlias = null)
    {
        EncodedUrl = encodedUrl;
        SuggestedAlias = suggestedAlias;
    }

    public CreateLinkRequest()
    {
    }

    /// <summary>
    ///     The custom part of the URL
    ///     must be between 3 and 30 characters long. Custom URLs are case sensitive and can
    ///     only consist of upper and lower case letters, numbers, underscores
    /// </summary>
    public string SuggestedAlias { get; set; }

    public string EncodedUrl { get; set; }
}