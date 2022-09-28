using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Magazine_Palpay.Web;
using Magazine_Palpay.Web.Models;
using Microsoft.AspNetCore.Http;
using Magazine_Palpay.Web.Extensions;
using System;
using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Magazine_Palpay.Web.IdentityModels;

namespace Magazine_Palpay.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<FluentUser> _userManager;
        public BookController(ApplicationDbContext context, UserManager<FluentUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("Admin/Book/Index")]
        public async Task<IActionResult> Index()
        {
            ViewBag.CategoryId = new SelectList(_context.BookCategory.Where(x => !x.IsDelete), "Id", "Name");
            return View();
        }

        [HttpPost("Admin/Book/LoadAll")]
        public async Task<JsonResult> LoadBookAsync(IFormCollection form)
        {
            string draw = Request.Form["draw"].FirstOrDefault();
            string start = Request.Form["start"].FirstOrDefault();
            string length = Request.Form["length"].FirstOrDefault();
            string sortColumn = Request.Form["columns[" + Request.Form["form[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            string sortColumnDirection = Request.Form["form[0][dir]"].FirstOrDefault();
            string name = form["Name"].ToString();
            string createdAt = form["CreatedAt"].ToString();
            int.TryParse(form["CategoryId"], out int categoryId);
            int page_start = int.Parse(start);
            int page_length = int.Parse(length);
            page_start = page_start / page_length;
            page_start = page_start + 1;

            string fromDate = !string.IsNullOrEmpty(createdAt) ? createdAt.Split('-')[0] : string.Empty;
            string toDate = !string.IsNullOrEmpty(createdAt) ? createdAt.Split('-')[1] : string.Empty;
            fromDate = fromDate.Replace(" ", string.Empty).Replace('/', '-');
            toDate = toDate.Replace(" ", string.Empty).Replace('/', '-');
            DateTime.TryParseExact(fromDate, "yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out DateTime dateFrom);

            DateTime.TryParseExact(toDate, "yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out DateTime dateTo);

            var queryable = _context.Book.Where(x => !x.IsDelete)
                .OrderByDescending(x => x.Id)
                .AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                queryable = queryable.Where(x => x.Name.Contains(name));
            }
            if (categoryId > 0)
            {
                queryable = queryable.Where(x => x.BookCategoryId.Equals(categoryId));
            }
            if (dateFrom != (DateTime)default)
            {
                queryable = queryable.Where(pc => pc.CreatedAt.Date >= dateFrom.Date);
            }

            if (dateTo != (DateTime)default)
            {
                queryable = queryable.Where(pc => pc.CreatedAt.Date <= dateTo.Date);
            }
            var productList = await queryable.ToPaginatedListAsync(page_start, page_length);
            var data = productList.Data.Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
                Category = _context.BookCategory.Find(x.BookCategoryId).Name,
                CreatedAt = x.CreatedAt
            }).ToList();
            var jsonData = new { data = data, recordsFiltered = productList.TotalCount, recordsTotal = productList.TotalCount };
            return new JsonResult(jsonData);
        }


        [HttpGet("Admin/Book/Create")]
        public IActionResult Create()
        {
            ViewData["BookCategoryId"] = new SelectList(_context.BookCategory.Where(x=>!x.IsDelete), "Id", "Name");
            return View();
        }

        [HttpPost("Admin/Book/Create")]
        public async Task<IActionResult> Create(Book book, IFormFile image, IFormFile linkFile)
        {
            if (ModelState.IsValid)
            {
                var fileName = string.Empty;
                var imgName = string.Empty;
                if (linkFile != null && linkFile.Length > 0)
                {
                    var file = await FormFileExtensions.SaveFile(linkFile, "UploadFiles");
                    fileName = file;
                }
                
                if (image != null && image.Length > 0)
                {
                    var img = await FormFileExtensions.SaveAsync(image, "UploadImages");
                    imgName = img;
                }
                book.Image = imgName;
                book.Link = fileName;
                book.CreatedBy = _userManager.GetUserId(User);
                book.CreatedAt = DateTime.Now;
                book.IsDelete = false;
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookCategoryId"] = new SelectList(_context.BookCategory.Where(x => !x.IsDelete), "Id", "Name", book.BookCategoryId);
            return View(book);
        }

        [HttpGet("Admin/Book/Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["BookCategoryId"] = new SelectList(_context.BookCategory.Where(x => !x.IsDelete), "Id", "Name", book.BookCategoryId);
            return View(book);
        }

        [HttpPost("Admin/Book/Edit")]
        public async Task<IActionResult> Edit(int id, Book book, IFormFile imageBook, IFormFile linkFile)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var fileName = string.Empty;
                    var imgName = string.Empty;
                    if (linkFile != null && linkFile.Length > 0)
                    {
                        var file = await FormFileExtensions.SaveFile(linkFile, "UploadFiles");
                        fileName = file;
                    }

                    if (imageBook != null && imageBook.Length > 0)
                    {
                        var img = await FormFileExtensions.SaveAsync(imageBook, "UploadImages");
                        imgName = img;
                    }
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        book.Link = fileName;
                    }
                    if (!string.IsNullOrEmpty(imgName))
                    {
                        book.Image = imgName;  
                    }
                    book.UpdatedBy = _userManager.GetUserId(User);
                    book.UpdatedAt = DateTime.Now;
                    book.IsDelete = false;
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookCategoryId"] = new SelectList(_context.BookCategory.Where(x => !x.IsDelete), "Id", "Name", book.BookCategoryId);
            return View(book);
        }

        [HttpPost("Admin/Book/Delete")]
        public async Task<JsonResult> OnPostDelete(int? id)
        {
            var book = await _context.Book.FindAsync(id);
            book.IsDelete = true;
            _context.Book.Update(book);
            await _context.SaveChangesAsync();
            Notify.Success("تمت عملية الحذف بنجاح");
            return new JsonResult(new
            {
                isValid = true,
                actionType = "redirect",
                redirectUrl = "/Admin/Book/Index"
            });
        }
        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
