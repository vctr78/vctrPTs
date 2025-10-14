using VisiblePT.Dtos;
using VisiblePT.Models;

namespace VisiblePT.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int Id);
        Task<Product> CreateProduct(ProductCreateDto dto);
        Task UpdateProduct(int Id, ProductUpdateDto dto);
        Task UpdateProductPrice(int Id, ProductUpdatePriceDto dto);
        Task UpdateProductDiscount(int Id, ProductUpdateDiscountDto dto);
        Task DeleteProduct(int Id);
    }
}
