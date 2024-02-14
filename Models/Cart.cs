using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Projet.Data;
using System;
using System.Collections.Generic;

namespace Projet.Models
{
    public class Cart
    {
        private readonly AppDbContext _context;

        public Cart(AppDbContext context)
        {
            _context = context;
        }

        public string Id { get; set; }

        public List<CartItem> CartItems { get; set; }

        public static Cart GetCart(IServiceProvider serviceProvider)
        {
            ISession session = serviceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = serviceProvider.GetService<AppDbContext>();

            string cartId = session.GetString("Id") ?? Guid.NewGuid().ToString();

            session.SetString("Id", cartId);

            return new Cart(context) { Id = cartId };
        }


        public CartItem GetCartItem(Book book)
        {
            return _context.CartItems.SingleOrDefault(ci => ci.Book.Id == book.Id && ci.CartId == Id);
        }

        public void addToCart(Book book, int quantity)
        {
            var cartItem = GetCartItem(book);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    Book = book,
                    Quantity = quantity,
                    CartId = Id,
                };

                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }

            _context.SaveChanges();
        }


        public int ReduceQuantity(Book book)
        {
            var cartItem = GetCartItem(book);

            var remainingQuantity = 0;

            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    remainingQuantity = --cartItem.Quantity;
                }
                else
                {
                    _context.Remove(cartItem);
                }
            }

            _context.SaveChanges();

            return remainingQuantity;
        }

        public int IncreaseQuantity(Book book)
        {
            var cartItem = GetCartItem(book);

            var remainingQuantity = 0;

            if (cartItem != null)
            {
                remainingQuantity = ++cartItem.Quantity;
            }

            _context.SaveChanges();

            return remainingQuantity;
        }


        public void RemoveFromCart(Book book)
        {
            var cartItem = GetCartItem(book);

            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
            }
            _context.SaveChanges();
        }

        public void ClearCart()
        {
            var cartItems = _context.CartItems.Where(ci => ci.CartId == Id);

            _context.CartItems.RemoveRange(cartItems);

            _context.SaveChanges();
        }

        public List<CartItem> GetCartItems()
        {
            

            var query = _context.CartItems.Where(ci => ci.CartId == Id)
                               .Include(ci => ci.Book)
                               .ToQueryString();
            Console.WriteLine(query);


            return CartItems ??
                (CartItems = _context.CartItems.Where(ci => ci.CartId == Id)
                .Include(ci => ci.Book)
                .ToList());
        }


        public int GetCartTotal()
        {
            return _context.CartItems
                .Where(ci => ci.CartId == Id)
                .Select(ci => ci.Book.Price * ci.Quantity)
                .Sum();
        }
    }
}
