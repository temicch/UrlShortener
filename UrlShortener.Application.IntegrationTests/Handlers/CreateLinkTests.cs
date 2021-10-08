using System;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FluentAssertions;
using FluentValidation;
using UrlShortener.Application.Implementation.ShortLinks.Commands.CreateLink;
using UrlShortener.Application.IntegrationTests.Common;
using UrlShortener.Common.Tests.TheoryData.Aliases;
using UrlShortener.Common.Tests.TheoryData.Urls;
using UrlShortener.WebUI;
using Xunit;

namespace UrlShortener.Application.IntegrationTests.Handlers
{
    public class CreateLinkTests : IntegrationTestBase
    {
        public CreateLinkTests(TestFixture<Startup> testFixture) : base(testFixture)
        {
        }

        [Theory]
        [ClassData(typeof(ValidUrls))]
        public async Task CreateLink_Successfully(string link)
        {
            // Assign

            // Act
            var result = await _mediator.Send(new CreateLinkRequest(link));

            // Assert
            result.IsSuccess.Should().BeTrue();
            _dbContext.ShortLinks.Where(x => x.Id == result.Value.Id).Should().NotBeEmpty();
        }

        [Fact]
        public async Task CreateLink_WithSameAliases_Failed()
        {
            // Assign
            const string alias = "mySuperAlias";
            var url = (string)new ValidUrls().First()[0];

            // Act
            Func<Task<IResult<CreateLinkResponse>>> result = async () =>
                await _mediator.Send(new CreateLinkRequest(url, alias));

            // Assert
            (await result()).IsSuccess.Should().BeTrue();
            (await result()).IsSuccess.Should().BeFalse();
        }

        [Theory]
        [ClassData(typeof(InvalidUrls))]
        public async Task CreateLink_WithInvalidUrls_Failed(string url)
        {
            // Assign

            // Act
            Func<Task> result = async () => await _mediator.Send(new CreateLinkRequest(url));

            // Assert
            await result.Should().ThrowAsync<ValidationException>();
        }

        [Theory]
        [ClassData(typeof(ValidAliases))]
        public async Task CreateLink_WithValidAlias_Successed(string alias)
        {
            // Assign
            var url = (string)new ValidUrls().First()[0];

            // Act
            Func<Task> result = async () => await _mediator.Send(new CreateLinkRequest(url, alias));

            // Assert
            await result.Should().NotThrowAsync<ValidationException>();
        }

        [Theory]
        [ClassData(typeof(InvalidAliases))]
        public async Task CreateLink_WithInvalidAlias_Failed(string alias)
        {
            // Assign
            var url = (string)new ValidUrls().First()[0];

            // Act
            Func<Task> result = async () => await _mediator.Send(new CreateLinkRequest(url, alias));

            // Assert
            await result.Should().ThrowAsync<ValidationException>();
        }

        [Theory]
        [ClassData(typeof(ValidUrls))]
        public async Task CreateLink_WithSameUrl_ReturnSameAliase(string url)
        {
            // Assign

            // Act
            var firstResult = await _mediator.Send(new CreateLinkRequest(url));
            var secondResult = await _mediator.Send(new CreateLinkRequest(url));

            // Assert
            firstResult.IsSuccess.Should().BeTrue();
            secondResult.IsSuccess.Should().BeTrue();
            firstResult.Value.Alias.Should().NotBeNullOrEmpty();
            firstResult.Value.Alias.Should().Be(secondResult.Value.Alias);
        }
    }
}