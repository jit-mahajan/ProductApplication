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
        public async Task<IEnumerable<Product>> GetAllAsync(int pageNumber, int pageSize)
        {

            var products = await _context.Products
                                  .Include(p => p.Category)
                                  .Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToListAsync();
            return products;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            _context.SaveChanges();
        }

        public async Task Update(Product model)
        {
            var product = await _context.Products.FindAsync(model.Id);
            if (product != null)
            {
                product.Name = model.Name;
                product.Price = model.Price;
                _context.Update(product);
                _context.SaveChanges();

            }
        }

        public async Task Delete(int id)
        {
            var product = await _context.Products
                          .Include(p => p.Category)
                          .FirstOrDefaultAsync(p => p.Id == id);
            _context.Products.Remove(product);
                _context.SaveChanges();
           

        }

        public async Task<int> TotalProducts()
        {
            return  await _context.Products.CountAsync();
        }

    }
}
