using BooksApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BooksApplication.Data
{
    public class BooksDBContext : DbContext
    {

        public BooksDBContext(DbContextOptions<BooksDBContext> options):base(options)

        { 
        
        
        }

        public DbSet<Category> Categories { get; set; }//corresponds to the sql table that will be created in the database. Each row in this table will be a category. And, the table will be called Categories


        public DbSet<Book> Books { get; set; }//adds the books table to the db

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(

                new Category
                {
                    CategoryId = 1,
                    Name = "Travel",
                    Description = "This is the description for the travel category"
                },
                    new Category
                    {
                        CategoryId = 2,
                        Name = "Technology",
                        Description = "This is the description for the technology category"
                    },
                    new Category
                    {
                        CategoryId = 3,
                        Name = "Fiction",
                        Description = "This is the description for the Fiction category"
                    }



                );
            modelBuilder.Entity<Book>().HasData(

                new Book
                {
                    BookId = 1,
                    BookTitle = "The Wager",
                    Author = "David Grann",
                    Description = "Tale of shipwreck, mutiny, and murder",
                    Price = 19.99m,
                    CategoryId = 1

                },
                new Book
                {
                    BookId = 2,
                    BookTitle = "Midnight",
                    Author = "Amy Mccullen",
                    Description = "In this pulse-pounding thriller, a once-in-a-lifetime trip to Antartica",
                    Price = 13.99m,
                    CategoryId = 2

                },
                        
                new Book
                {
                    BookId = 3,
                    BookTitle = "The Tusks of Extinction",
                    Author = "Ray Naylor",
                    Description = "Moscow has resurrected the mammoth. But someone must teach them how to be mammoths, or they are doomed to die out again",
                    Price = 25.99m,
                    CategoryId = 3

                }


                );
        }


    }
}
