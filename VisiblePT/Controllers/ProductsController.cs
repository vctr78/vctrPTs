using Microsoft.AspNetCore.Mvc;
using VisiblePT.Dtos;
using VisiblePT.Models;
using VisiblePT.Services;

namespace VisiblePT.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductService _service;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService service, ILogger<ProductsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetAll()
        {
            var products = _service.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{Id:int}", Name = "GetProductById")]
        public async Task<ActionResult<ProductResponseDto>> GetById(int Id)
        {
            var p = _service.GetProductById(Id);
            if (p == null) return NotFound();
            return Ok(MapToDto(await p));
        }

        [HttpPost]
        public async Task<ActionResult<ProductResponseDto>> Create([FromBody] ProductCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var created = await _service.CreateProduct(dto);
                var responseDto = MapToDto(created);
                return CreatedAtRoute("GetProductById", new { Id = responseDto.Id, responseDto });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{Id:int}")]
        public async Task<IActionResult> Update(int Id, [FromBody] ProductUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _service.UpdateProduct(Id, dto);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                await _service.DeleteProduct(Id);
                return NoContent();
            }
            catch(KeyNotFoundException)
            {
                return NotFound();
            }
        }

        //Mapper para mejorar seguridad
        private static ProductResponseDto MapToDto(Product p)
        {
            if (p == null) return null!;
            return new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                DiscountPercent = p.DiscountPercent,
                DiscountPrice = p.DiscountPrice,
                ImageURL = p.ImageURL,
                DateAdd = p.DateAdd
            };
        }
    }
}
