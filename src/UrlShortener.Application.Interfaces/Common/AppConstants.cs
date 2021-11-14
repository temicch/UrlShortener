using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Interfaces.Common;

/// <summary>
///     Application constants
/// </summary>
public static class AppConstants
{
    /// <summary>
    ///     Min string length of <see cref="ShortLink.Alias" />
    /// </summary>
    public const int ALIAS_MIN_LENGTH = 3;

    /// <summary>
    ///     Max string length of <see cref="ShortLink.Alias" />
    /// </summary>
    public const int ALIAS_MAX_LENGTH = 30;

    /// <summary>
    ///     Default string length of <see cref="ShortLink.Alias" />
    /// </summary>
    public const int ALIAS_DEFAULT_LENGTH = 7;
}
