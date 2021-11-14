using System;
using System.Linq;
using FluentAssertions;
using UrlShortener.Application.Interfaces.Paginated;
using Xunit;

namespace UrlShortener.Application.UnitTests;

public class PaginatedListTests
{
    [Theory]
    [InlineData(100, 20, 0, 100, true, false, 5)]
    [InlineData(100, 20, 1, 100, true, true, 5)]
    [InlineData(100, 20, 16, 100, false, true, 5)]
    [InlineData(100, 3, 5, 100, true, true, 34)]
    [InlineData(0, 10, 0, 0, false, false, 0)]
    public void Created_PagedList_Is_Have_CorrectProperties(int countOfElements,
        int pageSize,
        int pageIndex,
        int totalCount,
        bool hasNextPage,
        bool hasPrevPage,
        int totalPage)
    {
        // Assign
        var stringCollection = Enumerable
            .Range(0, countOfElements)
            .Select(i => i.ToString())
            .ToList();

        // Act
        var pagedList = new PaginatedList<string>(stringCollection, totalCount, pageIndex, pageSize);

        // Assert
        hasNextPage.Should().Be(pagedList.HasNextPage);
        hasPrevPage.Should().Be(pagedList.HasPreviousPage);
        totalPage.Should().Be(pagedList.TotalPages);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-1000)]
    public void PagedList_With_Negative_TotalCount_Cant_Be_Created(int totalCount)
    {
        // Assign

        // Act
        Action pagedListCreator = () => new PaginatedList<string>(null, totalCount, 50, 1);

        // Assert
        pagedListCreator.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData(100, 99, 1, true)]
    [InlineData(100, 100, 1, false)]
    [InlineData(100, 100, 10, false)]
    [InlineData(100, 0, 10, true)]
    [InlineData(1, 0, 10, true)]
    [InlineData(1, 1, 10, false)]
    [InlineData(0, 0, 10, false)]
    public void Created_PagedList_Returns_Correct_Flag_Of_Existing_Page(int totalCount,
        int pageIndex,
        int pageSize,
        bool isPageExists)
    {
        // Assign

        // Act
        var pagedList = new PaginatedList<string>(null, totalCount, pageIndex, pageSize);

        // Assert
        isPageExists.Should().Be(pagedList.IsPageExists);
    }
}
