using Microsoft.EntityFrameworkCore;
using ProductApplication.Data;
using ProductApplication.Models.Entity;
using ProductApplication.Service.IService;

namespace ProductApplication.Service.Service
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetAllAsync(int ? categoryId = null)
        {
            if (categoryId == null)
            {
                return await _context.Products.ToListAsync();
            }

            return await _context.Products
                                  .Where(P => P.CategoryId == categoryId)
                                  .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }
    }
}
