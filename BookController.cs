using BooksApplication.Data;
using BooksApplication.Models;
using BooksApplication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace BooksApplication.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class BookController : Controller
    {

        private BooksDBContext _dbContext;
        private IWebHostEnvironment _environment;




        public BookController(BooksDBContext context, IWebHostEnvironment environment)
        {
            _dbContext = context;//passes the dbContext object to the instance variable
            _environment = environment;

        }

        public IActionResult Index()
        {
            var ListOfBooks = _dbContext.Books.ToList();

            return View(ListOfBooks);
        }

        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> listOfCategories = _dbContext.Categories.ToList().Select(o => new SelectListItem
            {
                Text = o.Name,
                Value = o.CategoryId.ToString()

            }); //projection: allows us to project a category object to a selectlistitem object, where the name of the category is used as the text and the categoryId is used as the value of the SelectListItem

            //1) viewbag
            //ViewBag.listOfCategories = listOfCategories;

            //2) viewData
            //ViewData["listOfCategoriesVD"] = listOfCategories;

            //3) viewModel
            BookWithCategoriesVM bookWithCategoriesVM = new BookWithCategoriesVM();

            bookWithCategoriesVM.Book = new Book();

            bookWithCategoriesVM.ListOfCategories = listOfCategories;


            return View(bookWithCategoriesVM);
        }

        [HttpPost]
        public IActionResult Create(BookWithCategoriesVM bookWithCategoriesVM, IFormFile imgFile)
        {
            if (ModelState.IsValid)
            {
                string wwwrootPath = _environment.WebRootPath;
                if(imgFile != null)
                {
                    using (var filestream = new FileStream(Path.Combine(wwwrootPath,@"Images\bookImages\" + imgFile.FileName), FileMode.Create))        {

                        imgFile.CopyTo(filestream);//saves the file in the specified folder
                    
                    
                    
                   }

                    bookWithCategoriesVM.Book.ImgUrl = @"\Images\bookImages\" + imgFile.FileName;



                }


                _dbContext.Books.Add(bookWithCategoriesVM.Book);
                _dbContext.SaveChanges();

                return RedirectToAction("Index", "Book");

            }

            return View(bookWithCategoriesVM);
        }

        public BooksDBContext Get_dbContext()
        {
            return _dbContext;
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Book books = _dbContext.Books.Find(id);


            IEnumerable<SelectListItem> listOfCategories = _dbContext.Categories.ToList().Select(o => new SelectListItem
            {
                Text = o.Name,
                Value = o.CategoryId.ToString()

            });

            BookWithCategoriesVM bookWithCategoriesVM = new BookWithCategoriesVM();

            bookWithCategoriesVM.Book = books;

            bookWithCategoriesVM.ListOfCategories = listOfCategories;

            return View(bookWithCategoriesVM);



        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind("BookId, BookTitle, Author, Description, Price, ImgUrl, CategoryId")] Book book)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Books.Update(book);
                _dbContext.SaveChanges();

                return RedirectToAction("Index", "Book");
            }

            return View(book);

        }


    }
}
