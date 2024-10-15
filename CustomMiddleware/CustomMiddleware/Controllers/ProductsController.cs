using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomMiddleware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();

        }


        [HttpPost]
        public IActionResult CreateComment(Comment comment)
        {

            return Ok(comment);

        }

    }
}
