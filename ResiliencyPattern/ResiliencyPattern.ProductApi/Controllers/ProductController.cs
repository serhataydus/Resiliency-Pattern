using Microsoft.AspNetCore.Mvc;
using ResiliencyPattern.ProductApi.Services;

namespace ResiliencyPattern.ProductApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        private readonly StockService _stockService;

        public ProductController(ProductService productService, StockService stockService)
        {
            _productService = productService;
            _stockService = stockService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _productService.GetProduct(id));
        }

        [HttpGet("GetStock/{id}")]
        public async Task<IActionResult> GetStock(int id)
        {
            return Ok(await _stockService.GetStock(id));
        }
    }
}