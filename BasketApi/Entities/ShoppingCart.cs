namespace BasketApi.Entities
{
    public class ShoppingCart
    {
        public string Username { get; set; } = null!;
        public List<CartItem> CartItems { get; set; } = new();
        public decimal TotalPrice { get => CartItems.Sum(x => x.Quantity * x.Price); }

        public ShoppingCart()
        {

        }

        public ShoppingCart(string userName)
        {
            Username = userName;
        }

    }
}
