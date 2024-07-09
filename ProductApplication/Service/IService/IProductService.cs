using ProductApplication.Models.Entity;

namespace ProductApplication.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync(int? categoryId = null);

        Task<Product> GetByIdAsync(int id);
    }
}
