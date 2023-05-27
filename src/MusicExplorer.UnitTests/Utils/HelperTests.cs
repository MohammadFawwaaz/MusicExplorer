using MusicExplorer.Utils;

namespace MusicExplorer.UnitTests.Utils
{
    public class HelperTests
    {
        [Fact]
        public void PaginateResults_WithNonNullResults_ReturnsPaginationResult()
        {
            // Arrange
            var results = new[] { 1, 2, 3, 4, 5 };
            var pageNumber = 1;
            var pageSize = 2;

            // Act
            var paginationResult = Helper.PaginateResults(results, pageNumber, pageSize);

            // Assert
            Assert.NotNull(paginationResult);
            Assert.Equal(2, paginationResult.Results.Count);
            Assert.Equal(1, paginationResult.Page);
            Assert.Equal(2, paginationResult.PageSize);
            Assert.Equal(5, paginationResult.NumberOfSearchResults);
            Assert.Equal(3, paginationResult.NumberOfPages);
        }

        [Fact]
        public void PaginateResults_WithNullResults_ReturnsEmptyPaginationResult()
        {
            // Arrange
            IEnumerable<int> results = null;
            var pageNumber = 1;
            var pageSize = 2;

            // Act
            var paginationResult = Helper.PaginateResults(results, pageNumber, pageSize);

            // Assert
            Assert.NotNull(paginationResult);
            Assert.Null(paginationResult.Results);
            Assert.Equal(0, paginationResult.Page);
            Assert.Equal(0, paginationResult.PageSize);
            Assert.Equal(0, paginationResult.NumberOfSearchResults);
            Assert.Equal(0, paginationResult.NumberOfPages);
        }

        [Fact]
        public void PaginateResults_WithZeroPageNumber_ReturnsPaginationResultWithFirstPage()
        {
            // Arrange
            var results = new[] { 1, 2, 3, 4, 5 };
            var pageNumber = 0;
            var pageSize = 2;

            // Act
            var paginationResult = Helper.PaginateResults(results, pageNumber, pageSize);

            // Assert
            Assert.NotNull(paginationResult);
            Assert.Equal(2, paginationResult.Results.Count);
            Assert.Equal(1, paginationResult.Page);
            Assert.Equal(2, paginationResult.PageSize);
            Assert.Equal(5, paginationResult.NumberOfSearchResults);
            Assert.Equal(3, paginationResult.NumberOfPages);
        }

        [Fact]
        public void PaginateResults_WithZeroPageSize_ReturnsCorrectPaginationResult()
        {
            // Arrange
            var results = new[] { 1, 2, 3, 4, 5 };
            var pageNumber = 1;
            var pageSize = 0;

            // Act
            var paginationResult = Helper.PaginateResults(results, pageNumber, pageSize);

            // Assert
            Assert.NotNull(paginationResult);
            Assert.Equal(1, paginationResult.Page);
            Assert.Equal(5, paginationResult.PageSize);
            Assert.Equal(5, paginationResult.NumberOfSearchResults);
            Assert.Equal(1, paginationResult.NumberOfPages);
        }

        [Fact]
        public void PaginateResults_WithResultsCountLessThanPageSize_ReturnsPaginationResultWithAllResults()
        {
            // Arrange
            var results = new[] { 1, 2, 3 };
            var pageNumber = 1;
            var pageSize = 5;

            // Act
            var paginationResult = Helper.PaginateResults(results, pageNumber, pageSize);

            // Assert
            Assert.NotNull(paginationResult);
            Assert.Equal(3, paginationResult.Results.Count);
            Assert.Equal(1, paginationResult.Page);
            Assert.Equal(5, paginationResult.PageSize);
            Assert.Equal(3, paginationResult.NumberOfSearchResults);
            Assert.Equal(1, paginationResult.NumberOfPages);
        }

        [Fact]
        public void PaginateResults_WithPageNumberGreaterThanTotalPages_ReturnsEmptyPaginationResult()
        {
            // Arrange
            var results = new[] { 1, 2, 3, 4, 5 };
            var pageNumber = 10;
            var pageSize = 2;

            // Act
            var paginationResult = Helper.PaginateResults(results, pageNumber, pageSize);

            // Assert
            Assert.NotNull(paginationResult);
            Assert.Empty(paginationResult.Results);
            Assert.Equal(10, paginationResult.Page);
            Assert.Equal(2, paginationResult.PageSize);
            Assert.Equal(5, paginationResult.NumberOfSearchResults);
            Assert.Equal(3, paginationResult.NumberOfPages);
        }

        [Fact]
        public void PaginateResults_WithNegativePageNumber_ReturnsPaginationResultWithFirstPage()
        {
            // Arrange
            var results = new[] { 1, 2, 3, 4, 5 };
            var pageNumber = -1;
            var pageSize = 2;

            // Act
            var paginationResult = Helper.PaginateResults(results, pageNumber, pageSize);

            // Assert
            Assert.NotNull(paginationResult);
            Assert.Equal(2, paginationResult.Results.Count);
            Assert.Equal(1, paginationResult.Page);
            Assert.Equal(2, paginationResult.PageSize);
            Assert.Equal(5, paginationResult.NumberOfSearchResults);
            Assert.Equal(3, paginationResult.NumberOfPages);
        }
    }
}
