using BooksApplication.Data;
using BooksApplication.Models;
using BooksApplication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace BooksApplication.Controllers
{
    public class BookController : Controller
    {

        private BooksDBContext _dbContext;

        public BookController(BooksDBContext context)
        {
            _dbContext = context;//passes the dbContext object to the instance variable
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

                  }); //projection: allows us to project a categroy object to a selectlistitem object, where the name of the category is used as the text and the categoryId is used as the value of the SelectListItem

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
        public IActionResult Create(BookWithCategoriesVM bookWithCategoriesVM)
        {
            if(ModelState.IsValid)
            {
                _dbContext.Books.Add(bookWithCategoriesVM.Book);
                _dbContext.SaveChanges();

                return RedirectToAction("Index", "Book");

            }

            return View(bookWithCategoriesVM);
        }
    }
}
