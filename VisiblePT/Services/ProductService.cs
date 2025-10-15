using VisiblePT.Dtos;
using VisiblePT.Models;

namespace VisiblePT.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductHelper _helper;
        public ProductService(IProductHelper helper)
        {
            _helper = helper;
        }

        private static ProductResponseDto MapToDto(Product p)
        {
            return new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                DiscountPercent = p.DiscountPercent,
                DiscountPrice = p.DiscountPrice,
                ImageURL = p.ImageURL,
                DateAdd = p.DateAdd
            };
        }

        public async Task<IEnumerable<ProductResponseDto>> GetAllProducts()
        {
            var products = await _helper.GetAllProducts();
            return products.Select(MapToDto);
        }

        public async Task<Product> GetProductById(int Id)
        {
            return await _helper.GetProductById(Id);
        }

        public async Task<Product> CreateProduct(ProductCreateDto dto)
        {
            if (dto.Price <= 0)
            {
                throw new ArgumentException("El precio debe ser mayor que 0.");
            }

            var p = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                DiscountPercent = 0,
                ImageURL = dto.ImageURL
            };

            return await _helper.AddProduct(p);
        }

        public async Task UpdateProduct (int Id, ProductUpdateDto dto)
        {
            var p = await _helper.GetProductById(Id)?? throw new KeyNotFoundException("Producto no encontrado.");
            if (dto.Price <= 0)
            {
                throw new ArgumentException("El precio debe ser mayor que 0.");
            }

            p.Name = dto.Name;
            p.Description = dto.Description;
            p.ImageURL = dto.ImageURL;
            p.Price = dto.Price;

            await _helper.UpdateProduct(p);
        }

        public async Task UpdateProductPrice(int Id, ProductUpdatePriceDto dto)
        {
            if (dto.NewPrice <= 0)
            {
                throw new ArgumentException("El precio debe ser mayor que 0.");
            }

            await _helper.UpdatePriceById(Id, dto.NewPrice);
        }

        public async Task UpdateProductDiscount(int Id, ProductUpdateDiscountDto dto)
        {
            if (dto.DiscountPercent < 0)
            {
                throw new ArgumentException("El porcentaje de descuento no puede ser negativo.");
            }

            await _helper.UpdateDiscountById(Id, dto.DiscountPercent);
        }

        public async Task DeleteProduct(int Id)
        {
            var existe = await _helper.GetProductById(Id) ?? throw new KeyNotFoundException("Producto no encontrado.");
            await _helper.DeleteProduct(Id);
        }
    }
}
