using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Magazine_Palpay.Data;
using Magazine_Palpay.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Magazine_Palpay.Web.Extensions;

namespace Magazine_Palpay.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BookCategoryController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public BookCategoryController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("Admin/BookCategory/Index")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost("Admin/BookCategory/LoadAll")]
        public async Task<JsonResult> LoadBookCategoryAsync(IFormCollection form)
        {
            string draw = Request.Form["draw"].FirstOrDefault();
            string start = Request.Form["start"].FirstOrDefault();
            string length = Request.Form["length"].FirstOrDefault();
            string sortColumn = Request.Form["columns[" + Request.Form["form[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            string sortColumnDirection = Request.Form["form[0][dir]"].FirstOrDefault();
            string name = form["Name"].ToString();
            int page_start = int.Parse(start);
            int page_length = int.Parse(length);
            page_start = page_start / page_length;
            page_start = page_start + 1;
            var queryable = _context.BookCategory.Where(x => !x.IsDelete)
                .OrderByDescending(x => x.Id)
                .AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                queryable = queryable.Where(x => x.Name.Contains(name));
            }

            var productList = await queryable.ToPaginatedListAsync(page_start, page_length);
            var data = productList.Data.Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
                CreatedAt = x.CreatedAt
            }).ToList(); 
            var jsonData = new { data = data, recordsFiltered = productList.TotalCount, recordsTotal = productList.TotalCount };
            return new JsonResult(jsonData);

        }


        [HttpGet("Admin/BookCategory/Create")]
        public IActionResult Create()
        {
            return View();
        }

      
        [HttpPost("Admin/BookCategory/Create")]
        public async Task<IActionResult> Create(BookCategory bookCategory)
        {
            if (ModelState.IsValid)
            {
                bookCategory.CreatedBy = _userManager.GetUserId(User);
                bookCategory.CreatedAt = DateTime.Now;
                bookCategory.IsDelete = false;
                _context.Add(bookCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookCategory);
        }

        [HttpGet("Admin/BookCategory/Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookCategory = await _context.BookCategory.FindAsync(id);
            if (bookCategory == null)
            {
                return NotFound();
            }
            return View(bookCategory);
        }

     
        [HttpPost("Admin/BookCategory/Edit")] 
        public async Task<IActionResult> Edit(int id, BookCategory bookCategory)
        {
            if (id != bookCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bookCategory.UpdatedBy = _userManager.GetUserId(User);
                    bookCategory.UpdatedAt = DateTime.Now;
                    bookCategory.IsDelete = false;
                    _context.Update(bookCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookCategoryExists(bookCategory.Id))
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
            return View(bookCategory);
        }

     
        [HttpPost("Admin/BookCategory/Delete")]
        public async Task<JsonResult> OnPostDelete(int? id)
        {
            var bookCategory = await _context.BookCategory.FindAsync(id);
            bookCategory.IsDelete = true;
            _context.BookCategory.Update(bookCategory);
            await _context.SaveChangesAsync();
            Notify.Success("تمت عملية الحذف بنجاح");
            return new JsonResult(new
            {
                isValid = true,
                actionType = "redirect",
                redirectUrl = "/Admin/BookCategory/Index"
            });
        }

        private bool BookCategoryExists(int id)
        {
            return _context.BookCategory.Any(e => e.Id == id);
        }
    }
}
