using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using Shopping_Cart_UI.Data;
using Shopping_Cart_UI.Models;
using System.Data;

namespace Shopping_Cart_UI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;

        public CartRepository(ApplicationDbContext db , IHttpContextAccessor httpContextAccessor,
            UserManager<IdentityUser> userManager)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<bool> AddItem(int bookId,int quantity)
        { 
            using var transaction=_db.Database.BeginTransaction();
            try
            {
                string userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                {
                    return false;
                }
                var cart = await GetCart(userId);
                if (cart is null)
                {
                    cart = new ShoppingCart
                    {
                        UserId = userId,
                    };
                    _db.ShoppingCarts.Add(cart);

                }
                _db.SaveChanges();

                var cartIteam=_db.CartDetails.FirstOrDefault(a=>a.ShoppingCartId==cart.Id && a.BookId==bookId); 
                if (cartIteam is not null) {
                    cartIteam.Quantity += quantity;
                }
                else
                {
                    cartIteam = new CartDetail
                    {
                        BookId = bookId,
                        ShoppingCartId = cart.Id,
                        Quantity = quantity
                    };
                    _db.CartDetails.Add(cartIteam); 
                }
                _db.SaveChanges();
                transaction.Commit();   
                return true;    
            }
            catch (Exception ex) {  
            return false;
            }

        }

        public async Task<bool> RemoveItem(int bookId, int quantity)
        {
            //using var transaction = _db.Database.BeginTransaction();
            try
            {
                string userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                {
                    return false;
                }
                var cart = await GetCart(userId);
                if (cart is null)
                {
                    return false;

                }
                
                var cartIteam = _db.CartDetails.FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.BookId == bookId);
                if (cartIteam is  null)
                {
                    return false;
                }
                else if (cartIteam.Quantity==1)
                {
                    _db.CartDetails.Remove(cartIteam);
                }
                else
                {
                    cartIteam.Quantity=cartIteam.Quantity-1;        
                }
                _db.SaveChanges();
                //transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task <IEnumerable<ShoppingCart>> GetUserCart()
        {
            var userId= GetUserId();
            if (userId == null)
                throw new Exception("Invalid UserId");

            var shoppingCart=await _db.ShoppingCarts
                             .Include(a=>a.CartDetails)
                             .ThenInclude(a=>a.Book)
                             .ThenInclude(a=>a.Genre)
                             .Where(a=>a.UserId == userId).ToListAsync();    
            return shoppingCart;    
        }

        private async Task <ShoppingCart> GetCart(string userId)
        {
            var cart= await _db.ShoppingCarts.FirstOrDefaultAsync(s => s.UserId == userId);    
            return cart;
        }

        private string GetUserId()
        {
            var principle = _httpContextAccessor.HttpContext.User;
            string userId= _userManager.GetUserId(principle);
            return userId;
        }
    }
}
