using Microsoft.AspNetCore.Mvc;

namespace ResiliencyPattern.CategoryApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            return Ok(new { Id = id, Category = "Books" });
        }
    }
}