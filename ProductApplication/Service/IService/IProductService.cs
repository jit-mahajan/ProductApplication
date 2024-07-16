using ProductApplication.Models.Entity;

namespace ProductApplication.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync(int pageNumber, int pageSize);

        Task<Product> GetByIdAsync(int id);
        Task AddAsync(Product product);

        Task Update(Product product);
        Task Delete(int id);
        Task<int> TotalProducts();
    }
}
