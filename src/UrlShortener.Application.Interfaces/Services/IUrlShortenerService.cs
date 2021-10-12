using UrlShortener.Application.Interfaces.Common;

namespace UrlShortener.Application.Interfaces.Services
{
    /// <summary>
    ///     Represents service for shortening URL
    /// </summary>
    public interface IUrlShortenerService
    {
        /// <summary>
        ///     Short url if possible
        /// </summary>
        /// <param name="encodedUrl">Encoded url</param>
        /// <param name="alias">Alias of shorted url or <see langword="null" /></param>
        /// <param name="aliasLength">Alias length</param>
        /// <param name="salt">Encoding salt used for alias formation</param>
        /// <returns>
        ///     <see langword="true" /> if URL can be shorted, <see langword="false" /> otherwise
        /// </returns>
        public bool TryShortUrl(string encodedUrl, out string alias,
            int aliasLength = AppConstants.ALIAS_DEFAULT_LENGTH, string salt = "");

        /// <summary>
        ///     Normalize encoded URL string if it's correct URL
        /// </summary>
        /// <param name="encodedUrl">Encoded URL</param>
        /// <param name="normalizedUrl">Normalized URL or <see langword="null" /> if normalization failed</param>
        /// <returns><see langword="true" /> if normalize was successfull, <see langword="false" /> otherwise</returns>
        public bool TryNormalizeUrl(string encodedUrl, out string normalizedUrl);

        /// <summary>
        ///     <para>
        ///         URL validation
        ///     </para>
        ///     <para>
        ///         It use <see cref="TryNormalizeUrl(string)" /> for input encoded url
        ///     </para>
        /// </summary>
        /// <param name="encodedUrl"></param>
        /// <returns>
        ///     <see langword="true" /> if encoded URL valid, <see langword="false" /> otherwise
        /// </returns>
        public bool IsValidUrl(string encodedUrl);
    }
}