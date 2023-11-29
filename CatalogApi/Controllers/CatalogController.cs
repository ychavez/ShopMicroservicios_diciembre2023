using CatalogApi.Entities;
using CatalogApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CatalogApi.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class CatalogController(IProductRepository productRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
            => Ok(await productRepository.GetProducts());

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(string id)
        {
            var product = await productRepository.GetProduct(id);

            if (product is null)
                return NotFound();

            return Ok(product);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct([FromBody] Product product)
            => Ok(await productRepository.UpdateProduct(product));

        [HttpDelete]
        public async Task<ActionResult> DeleteProduct(string id)
            => Ok(await productRepository.DeleteProduct(id));

        [HttpPost]
        public async Task<ActionResult> CreateProduct([FromBody] Product product)
        {
            await productRepository.CreateProduct(product);
            return Ok();
        }
    }
}
