using Microsoft.AspNetCore.Mvc;
using Shopping_Cart_UI.Models;
using Shopping_Cart_UI.Models.DTO;
using Shopping_Cart_UI.Repository;
using System.Diagnostics;

namespace Shopping_Cart_UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository _homeRepository;

        public HomeController(ILogger<HomeController> logger,IHomeRepository homeRepository)
        {
            _logger = logger;
            _homeRepository = homeRepository;
        }

        public async Task<IActionResult> Index( string searchTerm = "", int genreId = 0)
        {

            IEnumerable<Book> books= await _homeRepository.GetBooks(searchTerm, genreId);
            IEnumerable<Genre> genres = await _homeRepository.Genres();
            BookDisplayModel bookModel = new BookDisplayModel
            {
                Books=books,
                Genres=genres,
                SearchTerm=searchTerm,  
                GenreId=genreId 
            };

            return View(bookModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
