namespace Shopping_Cart_UI.Models.DTO
{
    public class BookDisplayModel
    {
        public IEnumerable<Book> Books { get; set;}
        public IEnumerable<Genre> Genres { get; set;}
        public string SearchTerm { get; set; } = "";
        public int GenreId { get; set; } = 0;
    }
}
