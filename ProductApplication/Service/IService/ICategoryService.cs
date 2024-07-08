using ProductApplication.Models.Entity;
using ProductApplication.Service.Service;

namespace ProductApplication.Service.IService
{
    public interface ICategoryService
    {
  
       Task<IEnumerable<Category>> GetAllAsync();

       Task<Category> GetByIdAsync(int id);

       Task AddAsync(Category category);

        Task Update(Category model);
        Task Delete(int id);
    }
}
