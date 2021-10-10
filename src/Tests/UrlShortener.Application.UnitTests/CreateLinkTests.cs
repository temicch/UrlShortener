using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using UrlShortener.Application.Implementation.Services;
using UrlShortener.Application.Interfaces;
using UrlShortener.Application.Interfaces.Services;
using UrlShortener.Application.UseCases;
using UrlShortener.Application.UseCases.ShortLinks.Commands.CreateLink;
using UrlShortener.Common.Tests.TheoryData.Urls;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Events;
using Xunit;

namespace UrlShortener.Application.UnitTests
{
    public class CreateLinkTests
    {
        private readonly Mock<IDbContext> _dbContext;
        private readonly Mock<IDomainEventService> _domainEventService;
        private readonly IMapper _mapper;
        private readonly IUrlShortenerService _urlShortenerService = new UrlShortenerService();

        public CreateLinkTests()
        {
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()))
                .CreateMapper();
            _dbContext = new Mock<IDbContext>();
            _domainEventService = new Mock<IDomainEventService>();
        }

        [Fact]
        public async Task CreateLink_WithCorrectParams_Invoke_CreatedEvent()
        {
            // Assign
            var url = new ValidUrls().ToArray()[0][0] as string;
            var fakeData = Enumerable.Empty<ShortLink>().AsQueryable().BuildMockDbSet();
            _dbContext.Setup(x => x.ShortLinks).Returns(fakeData.Object);

            // Act
            var handler = new CreateLinkHandler(_dbContext.Object,
                _urlShortenerService,
                _mapper,
                _domainEventService.Object);
            var result = await handler.Handle(new CreateLinkRequest(WebUtility.UrlEncode(url)), default);

            // Assert
            _domainEventService.Verify(x => x.PublishAsync(It.IsAny<LinkCreatedEvent>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task CreateLink_WithIncorrectParams_NotInvoke_CreatedEvent()
        {
            // Assign
            var url = new InvalidUrls().ToArray()[0][0] as string;
            var fakeData = Enumerable.Empty<ShortLink>().AsQueryable().BuildMockDbSet();
            _dbContext.Setup(x => x.ShortLinks).Returns(fakeData.Object);

            // Act
            var handler = new CreateLinkHandler(_dbContext.Object,
                _urlShortenerService,
                _mapper,
                _domainEventService.Object);
            Func<Task> result = () => handler.Handle(new CreateLinkRequest(WebUtility.UrlEncode(url)), default);

            // Assert
            await result.Should().ThrowAsync<UriFormatException>();
            _domainEventService.Verify(x => x.PublishAsync(It.IsAny<LinkCreatedEvent>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }
    }
}