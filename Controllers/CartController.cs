using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projet.Data;
using Projet.Models;

namespace Projet.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        private readonly Cart _cart;

        public CartController(AppDbContext context, Cart cart)
        {
            _context = context;
            _cart = cart;
        }

        [Authorize]
        public IActionResult Index()
        {
            var cartItems = _cart.GetCartItems();

            _cart.CartItems = cartItems;

            return View(_cart);
        }

        public IActionResult RemoveFromCart(int id)
        {
            var selectedBook = _context.Books.FirstOrDefault(b => b.Id == id);

            if (selectedBook != null)
            {
                _cart.RemoveFromCart(selectedBook);
            }

            return RedirectToAction("Index");
        }

        public IActionResult ReduceQuantity(int id)
        {
            var selectedBook = _context.Books.FirstOrDefault(b => b.Id == id);

            if (selectedBook != null)
            {
                _cart.ReduceQuantity(selectedBook);
            }

            return RedirectToAction("Index");
        }

        public IActionResult IncreaseQuantity(int id)
        {
            var selectedBook = _context.Books.FirstOrDefault(b => b.Id == id);

            if (selectedBook != null)
            {
                _cart.IncreaseQuantity(selectedBook);
            }

            return RedirectToAction("Index");
        }


        public IActionResult ClearCart()
        {
            _cart.ClearCart();

            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult AddToCart(int id)
        {
            var selectedBook = _context.Books.FirstOrDefault(b => b.Id == id);

            if (selectedBook != null)
            {
                _cart.addToCart(selectedBook, 1);
            }

            return RedirectToAction("Index", "Store");
        }
    }
}
