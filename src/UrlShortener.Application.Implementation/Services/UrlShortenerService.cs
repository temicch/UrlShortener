using System;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using UrlShortener.Application.Interfaces.Services;

namespace UrlShortener.Application.Implementation.Services
{
    public class UrlShortenerService : IUrlShortenerService
    {
        public bool TryShortUrl(string encodedUrl, out string alias, string salt = "")
        {
            alias = string.Empty;

            if (string.IsNullOrEmpty(encodedUrl))
                return false;

            var encoded = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(encodedUrl + salt));

            alias = encoded.Length > 7 ? encoded[^7..] : encoded;

            return true;
        }

        public string NormalizeUrl(string encodedUrl)
        {
            var decodedUrl = WebUtility.UrlDecode(encodedUrl);

            Uri.TryCreate(decodedUrl, UriKind.Absolute, out var result);

            return result == null || !IsSupportedScheme(result.Scheme) ? string.Empty : result.AbsoluteUri;
        }

        public bool IsValidUrl(string encodedUrl)
        {
            return !string.IsNullOrEmpty(NormalizeUrl(encodedUrl));
        }

        /// <summary>
        ///     Support any scheme
        /// </summary>
        /// <param name="scheme"></param>
        /// <returns>
        ///     <see langword="true" /> if specified scheme supported,
        ///     <see langword="false" /> otherwise
        /// </returns>
        protected virtual bool IsSupportedScheme(string scheme)
        {
            return true;
        }
    }
}