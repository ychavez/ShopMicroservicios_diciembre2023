using BasketApi.Entities;

namespace BasketApi.Repositories
{
    public interface IBasketRepository
    {
        Task DeleteBasket(string username);

        Task<ShoppingCart?> GetBasket(string username);

        Task<ShoppingCart> UpdateBasket(ShoppingCart cart);
    }
}
