using ProductApplication.APIController.APIServices;
using ProductApplication.Models.Entity;

namespace ProductApplication.API.APIServices
{

    public class ProductApiService
    {
        private readonly HttpClient _httpClient;

        public ProductApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        
        public async Task<IEnumerable<Product>> GetProductsAsync(int pageNumber, int pageSize)
        {
           var response = await _httpClient.GetAsync($"/api/product?pageNumber={pageNumber}&pageSize={pageSize}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<Product>>();
            return result.Products;
        }
        
        public async Task<Product> GetProductByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/product/{id}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<Product>>();
            return await response.Content.ReadFromJsonAsync<Product>();
        }

        public async Task CreateProductAsync(Product product)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/product", product);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateProductAsync(Product product)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/product/{product.Id}", product);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteProductAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/product/{id}");
            response.EnsureSuccessStatusCode();
        }

    }
}
