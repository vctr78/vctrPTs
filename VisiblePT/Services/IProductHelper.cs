using VisiblePT.Models;

namespace VisiblePT.Services
{
    public interface IProductHelper
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int Id);
        Task<Product> AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(int Id);
        Task<Product> UpdatePriceById(int Id, decimal newPrice);
        Task<Product> UpdateDiscountById(int Id, int discount);
    }
}
