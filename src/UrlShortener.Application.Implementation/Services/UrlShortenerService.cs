using System;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using UrlShortener.Application.Interfaces.Common;
using UrlShortener.Application.Interfaces.Services;

namespace UrlShortener.Application.Implementation.Services;

public class UrlShortenerService : IUrlShortenerService
{
    public bool TryShortUrl(string encodedUrl, out string alias,
        int aliasLength = AppConstants.ALIAS_DEFAULT_LENGTH, string salt = "")
    {
        alias = string.Empty;

        if (string.IsNullOrEmpty(encodedUrl))
            return false;

        var encoded = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(encodedUrl + salt));

        alias = encoded.Length > aliasLength ? encoded[^aliasLength..] : encoded;

        return true;
    }

    public bool TryNormalizeUrl(string encodedUrl, out string normalizedUrl)
    {
        var decodedUrl = WebUtility.UrlDecode(encodedUrl);

        Uri.TryCreate(decodedUrl, UriKind.Absolute, out var result);

        var isSupportedScheme = IsSupportedScheme(result?.Scheme);
        normalizedUrl = isSupportedScheme ? result?.AbsoluteUri : null;

        return isSupportedScheme && normalizedUrl != null;
    }

    public bool IsValidUrl(string encodedUrl)
    {
        return TryNormalizeUrl(encodedUrl, out _);
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
