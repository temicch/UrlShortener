using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Application.UseCases.ShortLinks.Commands.CreateLink;
using UrlShortener.Application.UseCases.ShortLinks.Queries.GetLink;
using UrlShortener.Common.Tests.Common;
using UrlShortener.Common.Tests.TheoryData.Urls;
using UrlShortener.WebUI;
using Xunit;

namespace UrlShortener.Application.IntegrationTests.Handlers
{
    public class GetLinkTests : IntegrationTestBase
    {
        public GetLinkTests(TestFixture<Startup> testFixture) : base(testFixture)
        {
        }

        [Fact]
        public async Task GetLink_CreateClickEntity()
        {
            // Assign
            var linkClicksBefore = _dbContext.LinkClicks.Count();
            var link = await _mediator
                .Send(new CreateLinkRequest(new ValidUrls().First()[0].As<string>()));

            // Act
            var result = await _mediator.Send(new GetLinkRequest(link.Value.Alias));
            var linkClick = await _dbContext.LinkClicks
                .Where(x => x.LinkId == result.Value.Id)
                .FirstOrDefaultAsync();
            var linkClicksAfter = await _dbContext.LinkClicks.CountAsync();

            // Assert
            result.Should().NotBeNull();
            linkClick.Should().NotBeNull();
            linkClicksBefore.Should().Be(0);
            linkClicksAfter.Should().Be(1);
        }
    }
}