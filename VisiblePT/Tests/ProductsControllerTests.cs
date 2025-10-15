using Microsoft.AspNetCore.Mvc;
using Moq;
using VisiblePT.Controllers;
using VisiblePT.Dtos;
using VisiblePT.Models;
using VisiblePT.Services;
using Xunit;

namespace VisiblePT.Tests
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductService> _svcMock;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _svcMock = new Mock<IProductService>();
            var loggerMock = new Mock<ILogger<ProductsController>>();
            _controller = new ProductsController(_svcMock.Object, loggerMock.Object);
        }

        [Fact]
        public async Task GetById_Returns_NotFound_When_NoProduct()
        {
            _svcMock.Setup(s => s.GetProductById(1)).ReturnsAsync((Product)null!);
            var result = await _controller.GetById(1);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Create_Returns_CreatedAtRoute()
        {
            var dto = new ProductCreateDto { Name = "P", Description = "D", Price = 5m, ImageURL = "u" };
            var created = new Product { Id = 10, Name = dto.Name, Description = dto.Description, Price = dto.Price, DateAdd = DateTime.Now, DiscountPercent = 0, ImageURL = dto.ImageURL};

            _svcMock.Setup(s => s.CreateProduct(dto)).ReturnsAsync(created);

            var actionResult = await _controller.Create(dto);
            var createdResult = Assert.IsType<CreatedAtRouteResult>(actionResult.Result);
            Assert.Equal(201, createdResult.StatusCode);
        }
    }
}
