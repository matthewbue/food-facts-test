using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessFoods.Application.Services;
using FitnessFoods.Application.Interfaces;
using FitnessFoods.Domain.Entities;
using Xunit;

namespace FitnessFoods.UnitTests.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _mockRepository = new Mock<IProductRepository>();
            _productService = new ProductService(_mockRepository.Object);
        }

        [Fact]
        public async Task ListAllAsync_ShouldReturnListOfProducts()
        {
            var mockProducts = new List<Product>
            {
                new Product { Code = "0000000000017", ProductName = "Vitória crackers" },
                new Product { Code = "0000000000020", ProductName = "Donuts Simpson" }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync(It.IsAny<int>(), It.IsAny<int>()))
                           .ReturnsAsync(mockProducts);

            // Act: Call the service method
            var result = await _productService.ListAllAsync(1, 10);

            // Assert: Verify that the result contains the expected products
            Assert.Equal(2, result.Count);
            Assert.Equal("Vitória crackers", result[0].ProductName);
            Assert.Equal("Donuts Simpson", result[1].ProductName);
        }

        [Fact]
        public async Task GetByCodeAsync_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange: Mock the repository response
            var mockProduct = new Product { Code = "0000000000017", ProductName = "Vitória crackers" };

            _mockRepository.Setup(repo => repo.GetByCodeAsync(It.IsAny<string>()))
                           .ReturnsAsync(mockProduct);

            // Act: Call the service method
            var result = await _productService.GetByCodeAsync("0000000000017");

            // Assert: Verify that the result matches the expected product
            Assert.NotNull(result);
            Assert.Equal("Vitória crackers", result.ProductName);
        }

        [Fact]
        public async Task GetByCodeAsync_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange: Mock the repository response to return null
            _mockRepository.Setup(repo => repo.GetByCodeAsync(It.IsAny<string>()))
                           .ReturnsAsync((Product)null);

            // Act: Call the service method
            var result = await _productService.GetByCodeAsync("0000000000017");

            // Assert: Verify that the result is null
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldCallRepositoryUpdateMethod()
        {
            // Arrange: Create a mock product to update
            var mockProduct = new Product { Code = "0000000000017", ProductName = "Vitória crackers" };

            // Act: Call the service method
            await _productService.UpdateAsync("0000000000017", mockProduct);

            // Assert: Verify that the repository update method was called
            _mockRepository.Verify(repo => repo.UpdateAsync("0000000000017", mockProduct), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallRepositoryDeleteMethod()
        {
            // Arrange: Setup mock for soft delete
            var mockProduct = new Product { Code = "0000000000017", ProductName = "Vitória crackers" };

            // Act: Call the service method
            await _productService.DeleteAsync("0000000000017");

            // Assert: Verify that the repository soft delete method was called
            _mockRepository.Verify(repo => repo.SoftDeleteAsync("0000000000017"), Times.Once);
        }
    }
}
