using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductApplication.Models.Entity;
using ProductApplication.Service.IService;
using ProductApplication.Service.Service;

namespace ProductApplication.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _iProductService; 
        private readonly ICategoryService _iCategoryService;
        public ProductController(IProductService iProductService, ICategoryService iCategoryService)
        {
            _iProductService = iProductService;
            _iCategoryService = iCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _iProductService.GetAllAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> CreateOrEdit(int id)
        {

            await LoadCategories();
            if (id == 0)
            {
                return View(new Product());
            }
            else
            {
                try
                {
                    Product product = await _iProductService.GetByIdAsync(id);       
                    if (product != null)
                    {
                        return View(product);
                    }
                }
                catch (Exception ex)
                {
                    TempData["errorMessage"] = ex.Message;
                    return RedirectToAction("Index");
                }
            }
            TempData["errorMessage"] = $"Product details not found with Id {id}";
            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> CreateOrEdit(Product model)
        {
            try
            {

                if (model.Id == 0)
                {
                    await _iProductService.AddAsync(model);
                    TempData["successMessage"] = "Product Added  successfully";
                }
                else
                {
                    await _iProductService.Update(model);
                    TempData["successMessage"] = "Product Detail updated successfully";
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
                Product product = await _iProductService.GetByIdAsync(id);

                if (product != null)
                {
                    return View(product);
                }
            }
            catch (Exception ex)
            {
                TempData["error Message"] = ex.Message;
                return RedirectToAction("Index");
            }
            TempData["error Message"] = $"Product Details not found with Id : {id}";
            return RedirectToAction("Index");

        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _iProductService.Delete(id);
                TempData["successMessage"] = "Product deleted successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error Message"] = ex.Message;
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
