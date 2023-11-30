using BasketApi.Entities;
using BasketApi.Repositories;
using InventoryGrpc.Protos;
using Microsoft.AspNetCore.Mvc;

namespace BasketApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController(IBasketRepository basketRepository,
        Existence.ExistenceClient existenceClient) : ControllerBase
    {
        [HttpGet("{userName}")]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {


            var basket = await basketRepository.GetBasket(userName);

            return Ok(basket ?? new ShoppingCart(userName));
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart shoppingCart)
        {
            foreach (var item in shoppingCart.CartItems)
            {
                var existence = await existenceClient
                    .checkExistenceAsync(new() { Id = item.ProductId });

                item.Quantity =
                    item.Quantity > existence.ProductQTY ? existence.ProductQTY : item.Quantity;
            }

            await basketRepository.UpdateBasket(shoppingCart);
            return Ok(shoppingCart);
        }

        [HttpDelete("{userName}")]
        public async Task<ActionResult> DeleteBasket( string userName)
        {
            await basketRepository.DeleteBasket(userName);
            return NoContent();
        }

    }
}
