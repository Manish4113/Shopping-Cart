using Shopping_Cart_UI.Models;

namespace Shopping_Cart_UI.Repository
{
    public interface ICartRepository
    {
        Task<bool> AddItem(int bookId, int quantity);
        Task<bool> RemoveItem(int bookId, int quantity);
        Task<IEnumerable<ShoppingCart>> GetUserCart();
    }
}
