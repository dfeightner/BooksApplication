using BooksApplication.Data;
using BooksApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksApplication.Controllers
{
    public class CategoryController : Controller
    {
        private BooksDBContext _dbContext;

        public CategoryController(BooksDBContext dbContext)
        {

            _dbContext = dbContext;
        }


        public IActionResult Index()//list or fetch all objects
        {
           var ListOfCategories = _dbContext.Categories.ToList();

            return View(ListOfCategories);
        }

        [HttpGet]
        public IActionResult Create() 
        { 
            return View();
        
        }

        [HttpPost]
        public IActionResult Create(Category categoryobj)
        {
            //custom validation
            if(categoryobj.Name != null && categoryobj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("Name", "Category name cannot be 'test'");
            }


            //custom validation to make sure that name and description values are not the same
            if(categoryobj.Name == categoryobj.Description)
            {
                ModelState.AddModelError("Description", "Category Name and Description cannot be the same");
            }

            if(ModelState.IsValid)
            {
                _dbContext.Categories.Add(categoryobj);
                _dbContext.SaveChanges();

                return RedirectToAction("Index", "Category");
            }

            return View(categoryobj);
            
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Category category = _dbContext.Categories.Find(id);

            return View(category);

        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind("CategoryId, Name, Description")] Category categoryobj)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Update(categoryobj);
                _dbContext.SaveChanges();

                return RedirectToAction("Index", "Category");
            }

            return View(categoryobj);

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Category category = _dbContext.Categories.Find(id);

            return View(category);

        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePOST(int id)
        {
            Category category =_dbContext.Categories.Find(id);

            _dbContext.Categories.Remove(category); 
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Category");

        }

        [HttpGet]
        public IActionResult Details(int id)
        {
           
            Category categoryobj = _dbContext.Categories.Find(id); 

            return View(categoryobj);

        }


    }
}
