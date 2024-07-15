using Microsoft.EntityFrameworkCore;
using ProductApplication.Data;
using ProductApplication.Models.Entity;
using ProductApplication.Service.IService;

namespace ProductApplication.Service.Service
{
    public class CategoryService : ICategoryService
    {

        private readonly ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }
         public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
             _context.SaveChanges();
        }

        public  async Task<Category>GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task Update(Category model)
        {
            var category = await _context.Categories.FindAsync(model.Id);
            if (category != null)
            {
                category.Name = model.Name;
                category.IsActive = model.IsActive;
                _context.Update(category);
                _context.SaveChanges();

            }
        }

        public async Task Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }

        }


        public async Task<IEnumerable<Category>> LoadCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();

        }

    
    }
}
