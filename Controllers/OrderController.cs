using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projet.Data;
using Projet.Models;

namespace Projet.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly Cart _cart;

        public OrderController(AppDbContext context, Cart cart)
        {
            _context = context;
            _cart = cart;
        }
            
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var cartItems = _cart.GetCartItems();
            _cart.CartItems = cartItems;

            if (_cart.CartItems.Count == 0)
            {
                ModelState.AddModelError("", "Cart is empty, please add a book first.");
            }

            if (ModelState.IsValid)
            {
                CreateOrder(order);
                _cart.ClearCart();
                return View("CheckoutComplete", order);
            }

            return View(order);
        }

        public IActionResult CheckoutComplete(Order order)
        {
            return View(order);
        }

        public void CreateOrder(Order order)
        {
            order.OrderTime = DateTime.Now;

            foreach (var item in _cart.CartItems)
            {
                var orderItem = new OrderItem()
                {
                    Quantity = item.Quantity,
                    BookId = item.Book.Id,
                    OrderId = order.Id,
                    Price = item.Book.Price * item.Quantity
                };

                order.OrderItems.Add(orderItem);
                order.OrderTotal += orderItem.Price;
            }

            _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }
}
