using Shopping_Cart_UI.Models;

namespace Shopping_Cart_UI.Repository
{
    public interface ICartRepository
    {
        Task<int> AddItem(int bookId, int quantity);
        Task<int> RemoveItem(int bookId);
        Task<ShoppingCart> GetUserCart();
        Task<int> GetCartItemCount(string userId = "");
        Task<ShoppingCart> GetCart(string userId);
    }
}
    