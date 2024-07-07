using Microsoft.AspNetCore.Mvc;
using ProductApplication.Models;
using ProductApplication.Models.Entity;
using ProductApplication.Service;
using ProductApplication.Service.IService;
using System.Linq.Expressions;

namespace ProductApplication.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _iCategoryService;

        public CategoryController(ICategoryService iCategoryService)
        {
            _iCategoryService = iCategoryService;
  
        }
        public async Task<IActionResult> Index()
        {
            var category = await _iCategoryService.GetAllAsync();
            return View(category);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _iCategoryService.AddAsync(model);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Error Message"] = "Model state is Invalid";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
