using HW_MVC_Core_11_Roles_2.DbContexts;
using HW_MVC_Core_11_Roles_2.Models;
using Microsoft.EntityFrameworkCore;

namespace HW_MVC_Core_11_Roles_2.Services
{
    public interface IOrderProductsService
    {
        Task<IEnumerable<OrderProducts>> ReadAsync();
        Task<OrderProducts> GetByIdAsync(Guid? id);
        Task<OrderProducts> CreateAsync(OrderProducts orderProduct);
        Task<OrderProducts> UpdateAsync(Guid? id, OrderProducts orderProduct);
        Task<bool> DeleteAsync(Guid id);
    }
    public class OrderProductsService : IOrderProductsService
    {
        private readonly ProductDbContext _context;

        public OrderProductsService(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderProducts>> ReadAsync()
        {
            return await _context.OrderProducts
                .Include(op => op.Order)      // Include related Order data
                .Include(op => op.Product)    // Include related Product data
                .ToListAsync();               // Execute the query and return the results
        }

        public async Task<OrderProducts> GetByIdAsync(Guid? id)
        {
            if (id == null)
            {
                return null;
            }

            return await _context.OrderProducts.Include(op => op.Order).Include(op => op.Product)
                                               .FirstOrDefaultAsync(op => op.Id == id);
        }

        public async Task<OrderProducts> CreateAsync(OrderProducts orderProduct)
        {
            _context.OrderProducts.Add(orderProduct);
            await _context.SaveChangesAsync();
            return orderProduct;
        }

        public async Task<OrderProducts> UpdateAsync(Guid? id, OrderProducts orderProduct)
        {
            if (id != orderProduct.Id)
            {
                return null; // Or throw an exception depending on your error handling
            }
            _context.Entry(orderProduct).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return orderProduct;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var orderProduct = await _context.OrderProducts.FindAsync(id);
            if (orderProduct == null)
            {
                return false;
            }

            _context.OrderProducts.Remove(orderProduct);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
