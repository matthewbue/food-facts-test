using System.Threading.Tasks;
using FitnessFoods.Application.Services;
using FitnessFoods.Domain.Entities;
using FitnessFoods.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessFoods.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
            => Ok(await _service.ListAllAsync(page, pageSize));

        [HttpGet("{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var product = await _service.GetByCodeAsync(code);
            return product == null ? NotFound() : Ok(product);
        }

        [HttpPut("{code}")]
        public async Task<IActionResult> Update(string code, [FromBody] Product product)
        {
            await _service.UpdateAsync(code, product);
            return NoContent();
        }

        [HttpDelete("{code}")]
        public async Task<IActionResult> Delete(string code)
        {
            await _service.DeleteAsync(code);
            return NoContent();
        }
    }
}



