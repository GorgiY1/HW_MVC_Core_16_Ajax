using HW_MVC_Core_11_Roles_2.DbContexts;
using Microsoft.EntityFrameworkCore;
using Shop_app.Models;

namespace HW_MVC_Core_11_Roles_2.Services
{
    public interface IServiceProduct
    {
        Task<Product?> CreateAsync(Product? product);
        Task<IEnumerable<Product>> ReadAsync();
        Task<Product?> UpdateAsync(Guid? id,Product? product);
        Task<bool> DeleteAsync(Guid?id);
        Task<Product?> GetByIdAsync(Guid? id);
    }
    public class ServiceProduct : IServiceProduct
    {
        private readonly ProductDbContext _productContext;
        private readonly ILogger<ServiceProduct> _logger;
        public ServiceProduct(
            ProductDbContext context,
            ILogger<ServiceProduct> logger
            )
        {
            _productContext = context;
            _logger = logger;
        }
        public async Task<Product?> CreateAsync(Product? product)
        {
            if (product == null)
            {
                _logger.LogWarning("Attempt is add product null");
            }
            _ = await _productContext.Products.AddAsync(product);
            _ = await _productContext.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(Guid? id)
        {
            var product = await _productContext.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }
            _productContext.Products.Remove(product);
            await _productContext.SaveChangesAsync();
            return true;
        }

        public async Task<Product?> GetByIdAsync(Guid? id)
            => await _productContext.Products.FindAsync(id);

        public async Task<IEnumerable<Product>> ReadAsync()
            => await _productContext.Products.ToListAsync();

        public async Task<Product?> UpdateAsync(Guid? id, Product? product)
        {
            if (product == null || id != product.Id)
            {
                _logger.LogWarning("Attempt is update product or id [null]");
                return null;
            }
            try
            {
                _ = _productContext.Products.Update(product);
                _ = await _productContext.SaveChangesAsync();
                return product;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
    }
}
