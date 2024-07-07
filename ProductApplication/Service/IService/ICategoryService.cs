using ProductApplication.Models.Entity;
using ProductApplication.Service.Service;

namespace ProductApplication.Service.IService
{
    public interface ICategoryService
    {
  
       Task<IEnumerable<Category>> GetAllAsync();

       Task AddAsync(Category category);
    }
}
