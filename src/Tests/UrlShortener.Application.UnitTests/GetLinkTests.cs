using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using UrlShortener.Application.Interfaces;
using UrlShortener.Application.Interfaces.Services;
using UrlShortener.Application.UseCases;
using UrlShortener.Application.UseCases.ShortLinks.Queries.GetLink;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Events;
using Xunit;

namespace UrlShortener.Application.UnitTests;

public class GetLinkTests
{
    private readonly Mock<IDateTimeService> _dateTime;
    private readonly Mock<IDbContext> _dbContext;
    private readonly Mock<IDomainEventService> _domainEventService;
    private readonly IMapper _mapper;

    public GetLinkTests()
    {
        _mapper = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()))
            .CreateMapper();
        _dbContext = new Mock<IDbContext>();
        _domainEventService = new Mock<IDomainEventService>();
        _dateTime = new Mock<IDateTimeService>();
    }

    [Theory]
    [InlineData("alias")]
    public async Task GetLink_WithExistingAlias_Invoke_EventClick(string alias)
    {
        // Assign
        var fakeData = new[] { new ShortLink("url", alias) }.AsQueryable().BuildMockDbSet();
        _dbContext.Setup(x => x.ShortLinks).Returns(fakeData.Object);

        // Act
        var handler = new GetLinkHandler(_dbContext.Object,
            _domainEventService.Object,
            _dateTime.Object, _mapper);
        var result = await handler.Handle(new GetLinkRequest(alias), default);

        // Assert
        _domainEventService.Verify(x => x.PublishAsync(It.IsAny<LinkRequestedEvent>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetLink_WithoutRealAlias_Not_Invoke_EventClick()
    {
        // Assign
        var fakeData = new List<ShortLink>().AsQueryable().BuildMockDbSet();
        _dbContext.Setup(x => x.ShortLinks).Returns(fakeData.Object);

        // Act
        var handler = new GetLinkHandler(_dbContext.Object,
            _domainEventService.Object,
            _dateTime.Object, _mapper);
        var result = await handler.Handle(new GetLinkRequest(string.Empty), default);

        // Assert
        _domainEventService.Verify(x => x.PublishAsync(It.IsAny<LinkRequestedEvent>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
        result.IsFailure.Should().BeTrue();
    }
}
