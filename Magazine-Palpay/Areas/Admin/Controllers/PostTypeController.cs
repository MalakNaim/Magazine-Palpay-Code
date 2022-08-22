using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Magazine_Palpay.Data;
using Magazine_Palpay.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Magazine_Palpay.Web.ViewModels;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Magazine_Palpay.Web.Extensions;

namespace Magazine_Palpay.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PostTypeController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PostTypeController(ApplicationDbContext context, UserManager<IdentityUser> userManager, INotyfService _notifyInstance)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Admin/PostType
        [HttpGet("Admin/PostType/Index")]
        public async Task<IActionResult> Index()
        {
            var lst = await _context.PostType
                .Where(x => !x.IsDelete)
                .Select(x => new PostTypeViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Parent = x.ParentId == 0?"رئيسي":_context.PostType.Where(m=>m.Id==x.ParentId).FirstOrDefault().Name,
                    CreatedAt = x.CreatedAt
                }).ToListAsync();
            return View(lst);
        }

        [HttpPost("Admin/PostType/LoadAll")]
        public async Task<JsonResult> LoadPostTypeAsync(IFormCollection form)
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
            var queryable = _context.PostType.Where(x => !x.IsDelete)
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
                Parent = x.ParentId == 0 ? "رئيسي" : _context.PostType.Where(m => m.Id == x.ParentId).FirstOrDefault().Name,
                CreatedAt = x.CreatedAt
            }).ToList();
            var jsonData = new { data = data, recordsFiltered = productList.TotalCount, recordsTotal = productList.TotalCount };
            return new JsonResult(jsonData);
        }

        [HttpGet("Admin/PostType/Create")]
        public IActionResult Create()
        {
            var types = _context.PostType.Where(x => x.ParentId == 0 && !x.IsDelete).ToList();
            ViewBag.ParentId = new SelectList(types, "Id", "Name");
            return View();
        }
        
        [HttpPost("Admin/PostType/Create")]
        public async Task<IActionResult> Create(PostType postType)
        {
            if (ModelState.IsValid)
            {
                postType.CreatedBy = _userManager.GetUserId(User);
                postType.CreatedAt = DateTime.Now;
                postType.IsDelete = false;
                _context.Add(postType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(postType);
        }

        [HttpGet("Admin/PostType/Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            } 
            var postType = await _context.PostType.FindAsync(id);
            if (postType == null)
            {
                return NotFound();
            }
            var types = _context.PostType.Where(x => x.ParentId == 0 && !x.IsDelete).ToList();
            ViewBag.ParentId = new SelectList(types, "Id", "Name",postType.ParentId);
            return View(postType);
        }
      
        [HttpPost("Admin/PostType/Edit")]
        public async Task<IActionResult> Edit(int id, PostType postType)
        {
            if (id != postType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    postType.UpdatedBy = _userManager.GetUserId(User);
                    postType.UpdatedAt = DateTime.Now;
                    postType.IsDelete = false;
                    _context.Update(postType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostTypeExists(postType.Id))
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
            return View(postType);
        }
 
        [HttpPost("Admin/PostType/Delete")]
        public async Task<JsonResult> OnPostDelete(int? id)
        {
            var postType = await _context.PostType.FindAsync(id);
            postType.IsDelete = true;
            _context.PostType.Update(postType);
            await _context.SaveChangesAsync();
            Notify.Success("تمت عملية الحذف بنجاح");
            return new JsonResult(new
            {
                isValid = true,
                actionType = "redirect",
                redirectUrl = "/Admin/PostType/Index"
            });
        }


        private bool PostTypeExists(int id)
        {
            return _context.PostType.Any(e => e.Id == id);
        }
    }
}
