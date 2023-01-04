using Magazine_Palpay.Areas.Admin.Controllers;
using Magazine_Palpay.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Magazine_Palpay.Controllers
{
    public class BooksController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("Books/Index")]
        public IActionResult Index(int page = 0)
        {
            int countRow = _context.Book.Where(x => !x.IsDelete).Count();
            double perPage = 12;
            double NumberOfPages = Math.Ceiling(countRow / perPage);
            if (page < 1 || page > NumberOfPages)
            {
                page = 1;       
            }
            int skipValue = (page - 1) * (int)perPage;
            ViewBag.NumberOfPages = NumberOfPages;
            var books = _context.Book
                .Include(x => x.BookCategory)
            .OrderByDescending(x => x.CreatedAt)
            .Skip(skipValue).Take((int)perPage).ToList();
            ViewBag.page = page;
            //ViewBag.CategoryId = _context.BookCategory.Where(x=>!x.IsDelete).ToList(); 
            ViewBag.CategoryId = new SelectList( _context.BookCategory.Where(x=>!x.IsDelete).ToList(),"Id","Name"); 
            return View(books);
        }
        
        [HttpPost("Books/LoadAll")]
        public IActionResult LoadAll(IFormCollection form, int page = 0)
        {
            int.TryParse(form["category"], out int category);
            string searchKey = form["searchKey"];
            var books = _context.Book.Where(x => !x.IsDelete).AsQueryable();
            if (category != 0)
            {
                books = books.Where(x => x.BookCategoryId.Equals(category));
            }

            if (!string.IsNullOrEmpty(searchKey))
            {
                books = books.Where(x => x.Name.Contains(searchKey)); 
            }
            int countRow = books.Count();
            double perPage = 12;
            double NumberOfPages = Math.Ceiling(countRow / perPage);
            if (page < 1 || page > NumberOfPages)
            {
                page = 1;       
            }
            int skipValue = (page - 1) * (int)perPage;
            ViewBag.NumberOfPages = NumberOfPages;
            var bookList = books.Include(x => x.BookCategory)
            .OrderByDescending(x => x.CreatedAt)
            .Skip(skipValue).Take((int)perPage).ToList();
            ViewBag.page = page;
            //ViewBag.CategoryId = _context.BookCategory.Where(x=>!x.IsDelete).ToList(); 
            ViewBag.CategoryId = new SelectList( _context.BookCategory.Where(x=>!x.IsDelete).ToList(),"Id","Name", category);
            return View("Index", bookList);
        }
    }
}
