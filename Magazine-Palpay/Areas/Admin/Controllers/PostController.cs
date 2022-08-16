using Magazine_Palpay.Data;
using Magazine_Palpay.Data.Models;
using Magazine_Palpay.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Magazine_Palpay.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PostController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("Admin/Post/Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Admin/Post/Create")]
        public IActionResult Create()
        {
            var postType = _context.PostType.Where(x => !x.IsDelete && x.ParentId == 0).ToList();
            ViewBag.PostType = new SelectList(postType, "Id", "Name");
            return View();
        }

        [HttpGet("Admin/Post/GetSubPostType")]
        public IActionResult GetSubPostType(int? type)
        {
            var subPost = _context.PostType.Where(x => x.ParentId.Equals(type)).ToList();
            var result = subPost.Select(x => new
            {
                Id = x.Id,
                Name = x.Name
            });
            return Json(result);
        }

        [HttpGet("Admin/Post/Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            var postType = _context.PostType.Where(x => !x.IsDelete && x.ParentId == 0).ToList();
            ViewBag.PostType = new SelectList(postType, "Id", "Name",post.PostTypeId);
            var postSubType = _context.PostType.Where(x => !x.IsDelete && x.ParentId != 0).ToList(); 
            ViewBag.PostSubType = new SelectList(postSubType, "Id", "Name",post.PostSubType);
            return View(post);
        }

        [HttpPost("Admin/Post/OnPostCreateOrEdit")]
        public IActionResult OnPostCreateOrEdit(int id, Post post)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    post.CreatedAt = DateTime.Now;
                    post.CreatedBy = _userManager.GetUserId(User);
                    post.IsDelete = false;
                    _context.Post.Add(post);
                    _context.SaveChangesAsync();

                }
                else
                {
                    post.UpdatedAt = DateTime.Now;
                    post.UpdatedBy = _userManager.GetUserId(User);
                    _context.Post.Update(post);
                    _context.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
