using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using ProductApplication.Models.Entity;
using ProductApplication.Service.IService;

namespace ProductApplication.API.APIController
{
    [Route("api/[controller]")]
    public class ProductApiController : ControllerBase
    {

        private readonly IProductService _iProductService;

        public ProductApiController(IProductService iProductService)
        {
            _iProductService = iProductService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var products = await _iProductService.GetAllAsync(pageNumber, pageSize);
                int totalProducts = await _iProductService.TotalProducts();
                return Ok(new { products, totalProducts });

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
                var product = await _iProductService.GetByIdAsync(id);
                if (product == null)
                {
                    return NotFound("Product with Id {id} not found");
                }
                return Ok(product);
            }
            catch(Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _iProductService.AddAsync(model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product model)
        {
            if(id != model.Id)
            {
                return BadRequest("Product ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var product = _iProductService.GetByIdAsync(id);
                if (product == null)
                {
                    return NotFound($"Product with Id = {id} not found");
                }

                await _iProductService.Update(model);
                return NoContent();
            }
            catch(Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            try
            {
                var product = await _iProductService.GetByIdAsync(id);

                if(product == null)
                {
                    return NotFound($"Product with Id = {id} not found");
                }

                await _iProductService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

    }
}
