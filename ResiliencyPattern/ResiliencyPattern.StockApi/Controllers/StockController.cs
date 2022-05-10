using Microsoft.AspNetCore.Mvc;

namespace ResiliencyPattern.StockApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult GetStock(int id)
        {
            return Ok(new { Id = id, Stock = 5 });
        }
    }
}