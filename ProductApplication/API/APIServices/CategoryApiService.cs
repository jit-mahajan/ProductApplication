using ProductApplication.Models.Entity;

namespace ProductApplication.APIController.APIServices
{
    public class CategoryApiService
    {
        private readonly HttpClient _httpClient;

        public CategoryApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync(int pageNumber, int pageSize)
        {
            var response = await _httpClient.GetAsync($"/api/category?pageNumber={pageNumber}&pageSize={pageSize}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<Category>>();
            return result.Categories;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/category/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Category>();
        }

        public async Task CreateCategoryAsync(Category category)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/category", category);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/category/{category.Id}", category);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/category/{id}");
            response.EnsureSuccessStatusCode();
        }
    }

    public class ApiResponse<T>
    {
        public IEnumerable<T> Categories { get; set; }
        public int TotalCategories { get; set; }
    }

}

