using Moq;
using Xunit;
using VisiblePT.Dtos;
using VisiblePT.Models;
using VisiblePT.Services;

namespace VisiblePT.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductHelper> _helperMock;
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _helperMock = new Mock<IProductHelper>();
            _service = new ProductService(_helperMock.Object);
        }

        [Fact]
        public async Task CreateProduct_Should_Throw_WhenPriceIsZero()
        {
            var dto = new ProductCreateDto
            {
                Name = "P",
                Description = "D",
                Price = 0m,
                ImageURL = "http://img"
            };

            await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateProduct(dto));
        }

        [Fact]
        public async Task CreateProduct_Should_Call_Helper_AddProduct()
        {
            var dto = new ProductCreateDto
            {
                Name = "P",
                Description = "D",
                Price = 10m,
                ImageURL = "http://img"
            };

            _helperMock.Setup(h => h.AddProduct(It.IsAny<Product>()))
                       .ReturnsAsync((Product p) => { p.Id = 1; return p; });

            var result = await _service.CreateProduct(dto);

            _helperMock.Verify(h => h.AddProduct(It.IsAny<Product>()), Times.Once);
            Assert.Equal(1, result.Id);
            Assert.Equal(10m, result.Price);
        }
    }
}
