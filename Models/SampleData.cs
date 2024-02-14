using Microsoft.EntityFrameworkCore;
using Projet.Data;

namespace Projet.Models
{
    public class SampleData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }

                context.Books.AddRange(
                    new Book
                    {
                        Title = "To Kill a Mockingbird",
                        ISBN = "9780061120084",
                        PublishDate = DateTime.Parse("1960-7-11"),
                        Price = 120,
                        Author = "Harper Lee",
                        ImageUrl = "https://example.com/images/to-kill-a-mockingbird.jpg"
                    },

                    new Book
                    {
                        Title = "1984",
                        ISBN = "9780451524935",
                        PublishDate = DateTime.Parse("1949-6-8"),
                        Price = 99,
                        Author = "George Orwell",
                        ImageUrl = "https://example.com/images/1984.jpg"
                    },

                    new Book
                    {
                        Title = "Pride and Prejudice",
                        ISBN = "9780141439518",
                        PublishDate = DateTime.Parse("1813-1-28"),
                        Price = 85,
                        Author = "Jane Austen",
                        ImageUrl = "https://example.com/images/pride-and-prejudice.jpg"
                    },

                    new Book
                    {
                        Title = "The Great Gatsby",
                        ISBN = "9780743273565",
                        PublishDate = DateTime.Parse("1925-4-10"),
                        Price = 110,
                        Author = "F. Scott Fitzgerald",
                        ImageUrl = "https://example.com/images/great-gatsby.jpg"
                    },

                    new Book
                    {
                        Title = "The Catcher in the Rye",
                        ISBN = "9780316769480",
                        PublishDate = DateTime.Parse("1951-7-16"),
                        Price = 95,
                        Author = "J.D. Salinger",
                        ImageUrl = "https://example.com/images/catcher-in-the-rye.jpg"
                    },

                    new Book
                    {
                        Title = "The Hobbit",
                        ISBN = "9780345339683",
                        PublishDate = DateTime.Parse("1937-9-21"),
                        Price = 88,
                        Author = "J.R.R. Tolkien",
                        ImageUrl = "https://example.com/images/the-hobbit.jpg"
                    },

                    new Book
                    {
                        Title = "The Shining",
                        ISBN = "9780307743657",
                        PublishDate = DateTime.Parse("1977-1-28"),
                        Price = 105,
                        Author = "Stephen King",
                        ImageUrl = "https://example.com/images/the-shining.jpg"
                    },

                    new Book
                    {
                        Title = "The Alchemist",
                        ISBN = "9780061122415",
                        PublishDate = DateTime.Parse("1988-4-25"),
                        Price = 120,
                        Author = "Paulo Coelho",
                        ImageUrl = "https://example.com/images/the-alchemist.jpg"
                    },


                    new Book
                    {
                        Title = "The Lord of the Rings: The Two Towers",
                        ISBN = "9780618346257",
                        PublishDate = DateTime.Parse("1954-11-11"),
                        Price = 115,
                        Author = "J.R.R. Tolkien",
                        ImageUrl = "https://example.com/images/lotr-two-towers.jpg"
                    },

                    new Book
                    {
                        Title = "The Da Vinci Code",
                        ISBN = "9780307474278",
                        PublishDate = DateTime.Parse("2003-3-18"),
                        Price = 105,
                        Author = "Dan Brown",
                        ImageUrl = "https://example.com/images/da-vinci-code.jpg"
                    },

                    new Book
                    {
                        Title = "Harry Potter and the Sorcerer's Stone",
                        ISBN = "9780590353427",
                        PublishDate = DateTime.Parse("1997-6-26"),
                        Price = 95,
                        Author = "J.K. Rowling",
                        ImageUrl = "https://example.com/images/harry-potter.jpg"
                    },

                    new Book
                    {
                        Title = "The Hunger Games",
                        ISBN = "9780439023481",
                        PublishDate = DateTime.Parse("2008-9-14"),
                        Price = 88,
                        Author = "Suzanne Collins",
                        ImageUrl = "https://example.com/images/hunger-games.jpg"
                    }
                );

                context.SaveChanges();

            }
        }
    }
}
