using Microsoft.EntityFrameworkCore;
using VisiblePT.Data;
using VisiblePT.Models;

namespace VisiblePT.Services
{
    public class ProductHelper : IProductHelper
    {
        private readonly DataContext _context;

        public ProductHelper(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            IEnumerable<Product> products = await _context.Products.FromSqlRaw("EXEC SP_GetProducts").ToListAsync();
            return products;
        }

        public async Task<Product> GetProductById(int Id)
        {
            Product product = await _context.Products.FirstOrDefaultAsync(x => x.Id == Id);
            return product;
        }

        public async Task<Product> AddProduct(Product product)
        {
            product.DiscountPercent = 0;
            product.DateAdd = DateTime.Now;
            var entry = await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task UpdateProduct(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(int Id)
        {
            Product product = await GetProductById(Id);
            if(product == null)
            {
                return;
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> UpdatePriceById(int Id, decimal newPrice)
        {
            Product product = await GetProductById(Id)?? throw new KeyNotFoundException("Producto no encontrado");
            product.Price = newPrice;
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateDiscountById(int Id, int discount)
        {
            Product product = await GetProductById(Id) ?? throw new KeyNotFoundException("Producto no encontrado");
            product.DiscountPercent = discount;
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
