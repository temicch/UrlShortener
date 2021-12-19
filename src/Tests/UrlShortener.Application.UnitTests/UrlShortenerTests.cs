using System.Net;
using FluentAssertions;
using UrlShortener.Application.Implementation.Services;
using UrlShortener.Application.Interfaces.Services;
using UrlShortener.Common.Tests.TheoryData.Urls;
using Xunit;

namespace UrlShortener.Application.UnitTests;

public class UrlShortenerTests
{
    private readonly IUrlShortenerService _urlShortenerService;

    public UrlShortenerTests()
    {
        _urlShortenerService = new UrlShortenerService();
    }

    [Theory]
    [ClassData(typeof(ValidUrls))]
    public void Valid_URLs_Is_Shortened_Successfull(string url)
    {
        // Assign
        var encodedUrl = WebUtility.UrlEncode(url);
        _urlShortenerService.TryNormalizeUrl(encodedUrl, out var normalizedUrl);

        // Act
        var isValidUrl = _urlShortenerService.IsValidUrl(encodedUrl);
        var shortResult = _urlShortenerService.TryShortUrl(normalizedUrl, out var alias);

        // Assert
        isValidUrl.Should().BeTrue();
        normalizedUrl.Should().NotBeNullOrEmpty();
        alias.Should().NotBeNullOrEmpty();
        shortResult.Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(InvalidUrls))]
    public void Invalid_URLs_Is_Shortened_Unsuccessfull(string url)
    {
        // Assign
        var encodedUrl = WebUtility.UrlEncode(url);
        _urlShortenerService.TryNormalizeUrl(encodedUrl, out var normalizedUrl);

        // Act
        var isValidUrl = _urlShortenerService.IsValidUrl(encodedUrl);
        var shortResult = _urlShortenerService.TryShortUrl(normalizedUrl, out var alias);

        // Assert
        isValidUrl.Should().BeFalse();
        normalizedUrl.Should().BeNullOrEmpty();
        alias.Should().BeNullOrEmpty();
        shortResult.Should().BeFalse();
    }
}