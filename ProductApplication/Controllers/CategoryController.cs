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

        [HttpGet]
        public async Task<IActionResult> CreateOrEdit(int id)
        {
            if (id == 0)
            {
                return View(new Category());
            }
            else
            {
                try
                {
                    Category category = await _iCategoryService.GetByIdAsync(id);
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
            try
            {

                    if (model.Id == 0)
                    {
                        await _iCategoryService.AddAsync(model);
                        TempData["successMessage"] = "Category created successfully";
                    }
                    else 
                    {
                        await _iCategoryService.Update(model);
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

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Category category = await _iCategoryService.GetByIdAsync(id);

                if (category != null)
                {
                  //  _iCategoryService.Delete(id);
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
            try
            {
                await _iCategoryService.Delete(id);
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
