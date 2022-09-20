using Magazine_Palpay.Areas.Admin.Controllers;
using Magazine_Palpay.Web;
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
        public IActionResult Index(int? category, string searchKey, int page = 0)
        {
            int countRow = _context.Book.Where(x => !x.IsDelete).Count();
            double perPage = 6;
            double NumberOfPages = Math.Ceiling(countRow / perPage);
            if (page < 1 || page > NumberOfPages)
            {
                page = 1;
            }
            int skipValue = (page - 1) * (int)perPage;
            ViewBag.NumberOfPages = NumberOfPages;
            var books = _context.Book
                .Where(x =>!string.IsNullOrEmpty(searchKey) ? x.Name.Contains(searchKey) : true)
                .Where(x => !x.IsDelete && x.BookCategoryId == category
            || category == null || category == 0)
                .Include(x => x.BookCategory).OrderByDescending(x => x.CreatedAt)
            .Skip(skipValue).Take((int)perPage).ToList();
            ViewBag.page = page;
            ViewBag.CategoryId = _context.BookCategory.Where(x=>!x.IsDelete).ToList(); 
            return View(books);
        }
    }
}
