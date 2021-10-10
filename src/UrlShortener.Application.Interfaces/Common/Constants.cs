using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Interfaces.Common
{
    /// <summary>
    ///     Application constants
    /// </summary>
    public static class Constants
    {
        /// <summary>
        ///     Min string length of <see cref="ShortLink.Alias" />
        /// </summary>
        public const int ALIAS_MIN_LENGTH = 3;

        /// <summary>
        ///     Max string length of <see cref="ShortLink.Alias" />
        /// </summary>
        public const int ALIAS_MAX_LENGTH = 30;
    }
}