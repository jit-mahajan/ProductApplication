using ProductApplication.Models.Entity;

namespace ProductApplication.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product> GetByIdAsync(int id);
        Task AddAsync(Product product);

        Task Update(Product product);
        Task Delete(int id);
    }
}
