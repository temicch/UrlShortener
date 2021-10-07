﻿using System.Net;
using FluentAssertions;
using UrlShortener.Application.Implementation.Services;
using UrlShortener.Application.Interfaces.Services;
using UrlShortener.Common.Tests.TheoryData.Urls;
using Xunit;

namespace UrlShortener.Application.UnitTests
{
    public class UrlShortenerTests
    {
        public UrlShortenerTests()
        {
            _urlShortenerService = new UrlShortenerService();
        }

        private IUrlShortenerService _urlShortenerService { get; }

        [Theory]
        [ClassData(typeof(ValidUrls))]
        public void Valid_URLs_Is_Shortened_Successfull(string url)
        {
            // Assign
            var encodedUrl = WebUtility.UrlEncode(url);
            var normalizedUrl = _urlShortenerService.NormalizeUrl(encodedUrl);

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
            var normalizedUrl = _urlShortenerService.NormalizeUrl(encodedUrl);

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
}