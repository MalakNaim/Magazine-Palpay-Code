using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Magazine_Palpay.Data;
using Magazine_Palpay.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using Magazine_Palpay.Web.Extensions;

namespace Magazine_Palpay.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LastNewsController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public LastNewsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("Admin/LastNews/Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.LastNews.ToListAsync());
        } 

        [HttpPost("Admin/LastNews/LoadAll")]
        public async Task<JsonResult> LoadLastNewsAsync(IFormCollection form)
        {
            string draw = Request.Form["draw"].FirstOrDefault();
            string start = Request.Form["start"].FirstOrDefault();
            string length = Request.Form["length"].FirstOrDefault();
            string sortColumn = Request.Form["columns[" + Request.Form["form[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            string sortColumnDirection = Request.Form["form[0][dir]"].FirstOrDefault();
            string title = form["Title"].ToString();
            string createdAt = form["CreatedAt"].ToString();
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
            var queryable = _context.LastNews.Where(x => !x.IsDelete)
                .OrderByDescending(x => x.Id)
                .AsQueryable();
            if (!string.IsNullOrEmpty(title))
            {
                queryable = queryable.Where(x => x.Title.Contains(title));
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
                Title = x.Title,
                CreatedAt = x.CreatedAt
            }).ToList();

            var jsonData = new { data = data, recordsFiltered = productList.TotalCount, recordsTotal = productList.TotalCount };
            return new JsonResult(jsonData);

        }

        [HttpGet("Admin/LastNews/Create")]
        public IActionResult Create()
        {
            return View();
        }

         
        [HttpPost("Admin/LastNews/Create")]
        public async Task<IActionResult> Create(LastNews lastNews)
        {
            if (ModelState.IsValid)
            {
                lastNews.CreatedAt = DateTime.Now;
                lastNews.CreatedBy = _userManager.GetUserId(User);
                lastNews.IsDelete = false;
                _context.Add(lastNews);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lastNews);
        }

        [HttpGet("Admin/LastNews/Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lastNews = await _context.LastNews.FindAsync(id);
            if (lastNews == null)
            {
                return NotFound();
            }
            return View(lastNews);
        }

        [HttpPost("Admin/LastNews/Edit")]
        public async Task<IActionResult> Edit(int id, LastNews lastNews)
        {
            if (id != lastNews.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    lastNews.UpdatedAt = DateTime.Now;
                    lastNews.UpdatedBy = _userManager.GetUserId(User);
                    lastNews.IsDelete = false;
                    _context.Update(lastNews);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LastNewsExists(lastNews.Id))
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
            return View(lastNews);
        }


        [HttpPost("Admin/LastNews/Delete")]
        public async Task<JsonResult> OnPostDelete(int? id)
        {
            var lastNews = await _context.LastNews.FindAsync(id);
            lastNews.IsDelete = true;
            _context.LastNews.Update(lastNews);
            await _context.SaveChangesAsync();
            Notify.Success("تمت عملية الحذف بنجاح");
            return new JsonResult(new
            {
                isValid = true,
                actionType = "redirect",
                redirectUrl = string.Empty
            });
        }

        private bool LastNewsExists(int id)
        {
            return _context.LastNews.Any(e => e.Id == id);
        }
    }
}
