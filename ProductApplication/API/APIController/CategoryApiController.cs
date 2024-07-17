using Microsoft.AspNetCore.Mvc;
using ProductApplication.Models.Entity;
using ProductApplication.Service.IService;


namespace ProductApplication.API.APIController
{
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _iCategoryService;

        public CategoryController(ICategoryService iCategoryService)
        {
            _iCategoryService = iCategoryService;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var categories = await _iCategoryService.GetAllAsync(pageNumber, pageSize);
                int totalCategories = await _iCategoryService.TotalCategories();
                return Ok(new { categories, totalCategories });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

      
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var category = await _iCategoryService.GetByIdAsync(id);
                if (category == null)
                {
                    return NotFound($"Category with Id = {id} not found");
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        

        // POST: api/CategoryApi
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Category model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _iCategoryService.AddAsync(model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        

        // PUT: api/CategoryApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Category model)
        {
            if (id != model.Id)
            {
                return BadRequest("Category ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingCategory = await _iCategoryService.GetByIdAsync(id);
                if (existingCategory == null)
                {
                    return NotFound($"Category with Id = {id} not found");
                }

                await _iCategoryService.Update(model);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        
        // DELETE: api/CategoryApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var category = await _iCategoryService.GetByIdAsync(id);
                if (category == null)
                {
                    return NotFound($"Category with Id = {id} not found");
                }

                await _iCategoryService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
