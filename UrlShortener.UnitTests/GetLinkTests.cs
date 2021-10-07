using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using UrlShortener.Application.Implementation.ShortLinks.Queries.GetLink;
using UrlShortener.Application.Interfaces;
using UrlShortener.Application.Interfaces.Services;
using UrlShortener.Domain.Common;
using UrlShortener.Domain.Entities;
using Xunit;

namespace UrlShortener.Application.UnitTests
{
    public class GetLinkTests
    {
        [Theory]
        [InlineData("alias")]
        public async Task GetLink_WithExistingAlias_Invoke_EventClick(string alias)
        {
            // Assign
            var fakeData = new[] { new ShortLink("url", alias) }.AsQueryable().BuildMockDbSet();

            var dbContext = new Mock<IDbContext>();
            dbContext.Setup(x => x.ShortLinks).Returns(fakeData.Object);

            var domainEventService = new Mock<IDomainEventService>();
            var dateTime = new Mock<IDateTimeService>();

            // Act
            var handler = new GetLinkHandler(dbContext.Object, domainEventService.Object,
                dateTime.Object);
            var result = await handler.Handle(new GetLinkRequest(alias), default);

            // Assert
            domainEventService.Verify(x => x.PublishAsync(It.IsAny<DomainEvent<ShortLink>>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetLink_WithoutRealAlias_Not_Invoke_EventClick()
        {
            // Assign
            var fakeData = new List<ShortLink>().AsQueryable().BuildMockDbSet();

            var dbContext = new Mock<IDbContext>();
            dbContext.Setup(x => x.ShortLinks).Returns(fakeData.Object);

            var domainEventService = new Mock<IDomainEventService>();
            var dateTime = new Mock<IDateTimeService>();

            // Act
            var handler = new GetLinkHandler(dbContext.Object, domainEventService.Object,
                dateTime.Object);
            var result = await handler.Handle(new GetLinkRequest(string.Empty), default);

            // Assert
            domainEventService.Verify(x => x.PublishAsync(It.IsAny<DomainEvent<ShortLink>>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
            result.IsFailure.Should().BeTrue();
        }
    }
}