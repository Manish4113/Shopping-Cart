using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping_Cart_UI.Repository;

namespace Shopping_Cart_UI.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<IActionResult> AddItem(int bookId,int quantity=1, int redirect=0)
        {
            var cartCount=await _cartRepository.AddItem(bookId, quantity);  
            if(redirect == 0) { return Ok(cartCount); }
            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> RemoveItem(int bookId)
        {
            var cartCount = await _cartRepository.RemoveItem(bookId);
            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> GetUserCart()
        {
            var cart=await _cartRepository.GetUserCart();   
            return View(cart);
        }

        public async Task<IActionResult> GetTotalItemInCart()
        {
            var totalItem= await _cartRepository.GetCartItemCount();    
            return Ok(totalItem);
        }
        //Adding More Functinality.
    }
}

