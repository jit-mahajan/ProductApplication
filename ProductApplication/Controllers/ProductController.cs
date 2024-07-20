using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductApplication.API.APIServices;
using ProductApplication.Models.Entity;
using ProductApplication.Service.IService;
using ProductApplication.Service.Service;

namespace ProductApplication.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _iProductService; 
        private readonly ICategoryService _iCategoryService;
        private readonly IAppSettingsService _appSettingsService;
        private readonly ProductApiService _productApiService;
        private bool _useApi;
        public ProductController(IProductService iProductService, ICategoryService iCategoryService, IAppSettingsService appSettingsService, ProductApiService productApiService)
        {
            _iProductService = iProductService;
            _iCategoryService = iCategoryService;
            _appSettingsService = appSettingsService;
            _productApiService = productApiService;
        }
        private async Task InitializeSettingsAsync()
        {
            _useApi = await _appSettingsService.GetUseApiFlagAsync();
        }


        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            await InitializeSettingsAsync();

            IEnumerable<Product> products;
            int totalProducts;

            if (_useApi)
            {
                var response = await _productApiService.GetProductsAsync(pageNumber, pageSize);
                products = response;
                totalProducts = products.Count();
            }
            else
            {
                products = await _iProductService.GetAllAsync(pageNumber, pageSize);
                totalProducts = await _iProductService.TotalProducts();
            }

            var viewModel = new PaginatedProductViewModel
            {
                Products = products,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalProducts = totalProducts
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await InitializeSettingsAsync();
            await LoadCategories();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product model)
        {
            await InitializeSettingsAsync();
            try
            {
                await LoadCategories();
                if (_useApi)
                {
                    await _productApiService.CreateProductAsync(model);
                }
                else
                {
                    await _iProductService.AddAsync(model);
                }
                TempData["successMessage"] = "Product created successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            await InitializeSettingsAsync();
            await LoadCategories();
            if (id == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                Product product;
                if (_useApi)
                {
                    product = await _productApiService.GetProductByIdAsync(id);
                }
                else
                {
                    product = await _iProductService.GetByIdAsync(id);
                }
                if (product != null)
                {
                    return View(product);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            TempData["errorMessage"] = $"Product details not found with Id {id}";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Product model)
        {
            await InitializeSettingsAsync();
            if (id != model.Id)
            {
                TempData["errorMessage"] = "Invalid product ID";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                await LoadCategories();
                if (_useApi)
                {
                    await _productApiService.UpdateProductAsync(model);
                }
                else
                {
                    await _iProductService.Update(model);
                }
                TempData["successMessage"] = "Product updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await InitializeSettingsAsync();
            try
            {
                Product product;
                if (_useApi)
                {
                    product = await _productApiService.GetProductByIdAsync(id);
                }
                else
                {
                    product = await _iProductService.GetByIdAsync(id);
                }

                if (product != null)
                {
                    return View(product);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            TempData["errorMessage"] = $"Product details not found with Id {id}";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await InitializeSettingsAsync();
            try
            {
                if (_useApi)
                {
                    await _productApiService.DeleteProductAsync(id);
                }
                else
                {
                    await _iProductService.Delete(id);
                }
                TempData["successMessage"] = "Product deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [NonAction]
        private async Task LoadCategories()
        {
            var categories = await _iCategoryService.LoadCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
        }
    }
}
