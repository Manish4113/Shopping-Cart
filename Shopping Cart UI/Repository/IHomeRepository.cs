using Shopping_Cart_UI.Models;

namespace Shopping_Cart_UI.Repository
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Book>> GetBooks(string searchTerm = "", int genreId = 0);
        Task<IEnumerable<Genre>> Genres();
    }
}