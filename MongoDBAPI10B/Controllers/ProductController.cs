using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MongoDBAPI.Model;
using MongoDBAPI10B.Repositories;

namespace MongoDBAPI10B.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private IProductCollection db = new ProductCollection();

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await db.GetAllProducts());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductDetails(string id)
        {
            return Ok(await db.GetProductById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (product == null)
                return BadRequest();

            if (string.IsNullOrWhiteSpace(product.Name))
            {
                ModelState.AddModelError("Name", "The product shouldn't be empty");
                return BadRequest(ModelState);
            }

            await db.InsertProduct(product);
            return CreatedAtAction(nameof(GetProductDetails), new { id = product.Id }, product);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Product product, string id)
        {
            if (product == null)
                return BadRequest();

            if (product.Name == string.Empty)
            {
                ModelState.AddModelError("Name", "The product shouldn't be empty");
            }

            product.Id=id;
            await db.UpdateProduct(product);
            return Created("Created", true);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await db.DeleteProduct(id);
            return NoContent();
        }
    }
}
