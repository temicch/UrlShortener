using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Application.Interfaces.Paginated;
using UrlShortener.Application.UseCases.ShortLinks.Commands.CreateLink;
using UrlShortener.Application.UseCases.ShortLinks.Queries.GetLinks;
using UrlShortener.Common.Tests.Common;
using UrlShortener.Common.Tests.Factories;
using UrlShortener.Common.Tests.TheoryData.Aliases;
using UrlShortener.Common.Tests.TheoryData.Urls;
using UrlShortener.WebUI.Controllers;
using Xunit;

namespace UrlShortener.WebUI.IntegrationTests;

public class LinksControllerTests : IntegrationTestBase
{
    private readonly LinksController _controller;

    public LinksControllerTests(TestFixture<Startup> testFixture) : base(testFixture)
    {
        _controller = new LinksController(_mediator);
    }

    [Theory]
    [ClassData(typeof(ValidAliases))]
    public async Task GetLink_WithNonExistsAlias_Returns_NotFoundResult(string alias)
    {
        // Act
        var result = await _controller.GetLink(alias);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Theory]
    [ClassData(typeof(InvalidAliases))]
    public async Task GetLink_WithInvalidAlias_ThrowException(string alias)
    {
        // Act
        Func<Task> result = () => _controller.GetLink(alias);

        // Assert
        await result.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task GetLinks_WithEmptyContext_Returns_EmptyPaginatedList()
    {
        // Assign
        var emptyDbContextLength = await _dbContext.ShortLinks.CountAsync();

        // Act
        var response = await _controller.GetLinks(new GetLinksRequest());

        // Assert
        emptyDbContextLength.Should().Be(0);
        response.As<JsonResult>()
            .Value.As<PaginatedList<GetLinksResponse>>()
            .Items.Count.Should().Be(0);
    }

    [Fact]
    public async Task GetLinks_WithNonEmptyContext_Returns_NonEmptyPaginatedList()
    {
        // Assign
        var links = EntitiesFactory.GetValidShortLinks();
        var linksCount = links.Count();
        await _dbContext.ShortLinks.AddRangeAsync(links);
        await _dbContext.SaveChangesAsync(default);
        var dbContextLength = await _dbContext.ShortLinks.CountAsync();

        // Act
        var response = await _controller.GetLinks(new GetLinksRequest()
        {
            PageIndex = 0,
            PageSize = linksCount
        });

        // Assert
        dbContextLength.Should().BePositive().And.Be(linksCount);
        response.As<JsonResult>()
            .Value.As<PaginatedList<GetLinksResponse>>()
            .Items.Count.Should().Be(linksCount);
    }

    [Theory]
    [ClassData(typeof(ValidUrls))]
    public async Task CreateLink_WithValidParams_Returns_OkResult(string url)
    {
        // Assign
        var encodedUrl = WebUtility.UrlEncode(url);

        // Act
        var response = await _controller.CreateLink(new CreateLinkRequest(encodedUrl));

        // Assert
        response.Should().BeOfType<OkObjectResult>();
    }

    [Theory]
    [ClassData(typeof(InvalidUrls))]
    public async Task CreateLink_WithInvalidParams_ThrowException(string url)
    {
        // Assign
        var encodedUrl = WebUtility.UrlEncode(url);

        // Act
        Func<Task> result = () => _controller.CreateLink(new CreateLinkRequest(encodedUrl));

        // Assert
        await result.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task CreateLink_WithExistsAlias_ReturnsFailure()
    {
        // Assign
        var link = EntitiesFactory.GetValidShortLinks().ElementAt(0);
        await _dbContext.ShortLinks.AddAsync(link);
        await _dbContext.SaveChangesAsync(default);

        // Act
        var result = await _controller.CreateLink(new CreateLinkRequest(WebUtility.UrlEncode(link.Link),
            link.Alias));

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    public override void Dispose()
    {
        _controller.Dispose();
        base.Dispose();
    }
}
