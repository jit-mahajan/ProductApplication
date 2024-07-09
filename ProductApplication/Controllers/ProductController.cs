using Microsoft.AspNetCore.Mvc;
using ProductApplication.Service.IService;
using ProductApplication.Service.Service;

namespace ProductApplication.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _iProductService; 
        public ProductController(IProductService iProductService)
        {
            _iProductService = iProductService;
        }

        public async Task<IActionResult> Index()
        {
            var category = await _iProductService.GetAllAsync();
            return View(category);
        }


    }
}
