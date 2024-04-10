using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shopping_Cart_UI.Models;

namespace Shopping_Cart_UI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Genre> Genres { get; set; }        
        public DbSet<Book> Books { get; set; }  
        public DbSet<ShoppingCart> ShoppingCarts { get; set;}
        public DbSet<CartDetail> CartDetails { get; set;}
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderDetail> ordersDetails { get; set; }
        public DbSet<OrderStatus> ordersStatus { get; set; }


    }
}
