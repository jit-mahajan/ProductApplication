using Microsoft.AspNetCore.Mvc;
using ProductApplication.APIController.APIServices;
using ProductApplication.Models;
using ProductApplication.Models.Entity;
using ProductApplication.Service;
using ProductApplication.Service.IService;
using System.Linq.Expressions;
using ProductApplication.API;
using ProductApplication.APIController;

namespace ProductApplication.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _iCategoryService;
        private readonly CategoryApiService _categoryApiService;
        private readonly IAppSettingsService _appSettingsService;
        private  bool _useApi;

        public CategoryController(ICategoryService iCategoryService, CategoryApiService categoryApiService, IAppSettingsService appSettingsService)
        {
            _iCategoryService = iCategoryService;
            _categoryApiService = categoryApiService;
            _appSettingsService = appSettingsService;

        }

        private async Task InitializeSettingsAsync()
        {
            _useApi = await _appSettingsService.GetUseApiFlagAsync();
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            await InitializeSettingsAsync();
            IEnumerable<Category> categories;
            int totalCategories;
            if (_useApi)
            {
                var response = await _categoryApiService.GetCategoriesAsync(pageNumber, pageSize);
                categories = response;
                totalCategories = categories.Count();
            }
            else
            {

                categories = await _iCategoryService.GetAllAsync(pageNumber, pageSize);
                totalCategories = await _iCategoryService.TotalCategories();
            }
            var viewModel = new PaginatedCategoryViewModel
            {
                Categories = categories,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalProducts = totalCategories
            };

            return View(viewModel);
        }

        /*
        [HttpGet]
        public async Task<IActionResult> CreateOrEdit(int id)
        {
            await InitializeSettingsAsync();
            if (id == 0)
            {
                return View(new Category());
            }
            else
            {
                try
                {
                    Category category;
                    if (_useApi)
                    {
                        category = await _categoryApiService.GetCategoryByIdAsync(id);
                    }
                    else
                    {
                        category = await _iCategoryService.GetByIdAsync(id);
                    }
                    if (category != null)
                    {
                        return View(category);
                    }
                }
                catch (Exception ex)
                {
                    TempData["errorMessage"] = ex.Message;                   
                    return RedirectToAction("Index");
                }
            }
            TempData["errorMessage"] = $"Category details not found with Id {id}";
            return RedirectToAction("Index");
           
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrEdit(Category model)
        {

            await InitializeSettingsAsync();
            try
            {

                    if (model.Id == 0)
                    {

                        if (_useApi)
                        {
                            await _categoryApiService.CreateCategoryAsync(model);
                        }
                        else
                        {
                            await _iCategoryService.AddAsync(model);
                        }
                        TempData["successMessage"] = "Category created successfully";
                    }
                    else 
                    {
                        if (_useApi)
                        {
                            await _categoryApiService.UpdateCategoryAsync(model);
                        }
                        else
                        {
                            await _iCategoryService.Update(model);
                        }
                        TempData["successMessage"] = "Category Detail updated successfully";
                    }
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        */

        [HttpGet]
        public async Task<IActionResult> CreateCategory()
        {
            await InitializeSettingsAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category model)
        {
            try
            {
                await InitializeSettingsAsync();
                  if (_useApi)
                    {
                        await _categoryApiService.CreateCategoryAsync(model);
                    }
                    else
                    {
                        await _iCategoryService.AddAsync(model);
                    }
                    TempData["successMessage"] = "Category created successfully";
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
            if (id == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                Category category;
                if (_useApi)
                {
                    category = await _categoryApiService.GetCategoryByIdAsync(id);
                }
                else
                {
                    category = await _iCategoryService.GetByIdAsync(id);
                }
                if (category != null)
                {
                    return View(category);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            TempData["errorMessage"] = $"Category details not found with Id {id}";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category model)
        {
            try
            {
                await InitializeSettingsAsync();
                if (ModelState.IsValid)
                {
                    if (_useApi)
                    {
                        await _categoryApiService.UpdateCategoryAsync(model);
                    }
                    else
                    {
                        await _iCategoryService.Update(model);
                    }
                    TempData["successMessage"] = "Category updated successfully";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return View("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await InitializeSettingsAsync();
            try
            {
                Category category;

                if (_useApi)
                {
                    category = await _categoryApiService.GetCategoryByIdAsync(id);
                }
                else
                {
                    category = await _iCategoryService.GetByIdAsync(id);
                }

                if (category != null)
                {
                    return View(category);
                }
            }
            catch (Exception ex)
            {
                TempData["error Message"] = ex.Message;
                return RedirectToAction("Index");
            }
            TempData["error Message"] = $"Category Details not found with Id : {id}";
            return RedirectToAction("Index");

        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await InitializeSettingsAsync();
            try
            {
                if (_useApi)
                {
                    await _categoryApiService.DeleteCategoryAsync(id);
                }
                else
                {
                    await _iCategoryService.Delete(id);
                }
                TempData["successMessage"] = "Category deleted successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error Message"] = ex.Message;
                return View();
            }
        }


    }
}
