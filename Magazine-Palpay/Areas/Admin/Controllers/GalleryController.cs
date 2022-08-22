using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Magazine_Palpay.Data;
using Magazine_Palpay.Data.Models;
using Microsoft.AspNetCore.Http;
using Magazine_Palpay.Web.Extensions;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Magazine_Palpay.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class GalleryController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public GalleryController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

         [HttpGet("Admin/Gallery/Index")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost("Admin/Gallery/LoadAll")]
        public async Task<JsonResult> LoadPostsAsync(IFormCollection form)
        {
            string draw = Request.Form["draw"].FirstOrDefault();
            string start = Request.Form["start"].FirstOrDefault();
            string length = Request.Form["length"].FirstOrDefault();
            string sortColumn = Request.Form["columns[" + Request.Form["form[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            string sortColumnDirection = Request.Form["form[0][dir]"].FirstOrDefault();
            string title = form["Title"].ToString();
            int page_start = int.Parse(start);
            int page_length = int.Parse(length);
            page_start = page_start / page_length;
            page_start = page_start + 1;

            var queryable = _context.Gallery.Where(x => !x.IsDelete)
                .OrderByDescending(x => x.Id)
                .AsQueryable();
            if (!string.IsNullOrEmpty(title))
            {
                queryable = queryable.Where(x => x.Title.Contains(title));
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


        [HttpGet("Admin/Gallery/Create")]
        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost("Admin/Gallery/Create")]
         public async Task<IActionResult> Create(Gallery gallery, List<IFormFile> galleryPhotos)
        {
            if (ModelState.IsValid)
            { 
                gallery.CreatedAt = DateTime.Now;
                gallery.CreatedBy = _userManager.GetUserId(User);
                gallery.IsDelete = false;
                _context.Add(gallery);
                await _context.SaveChangesAsync();
                foreach (var photo in galleryPhotos)
                {
                    if (photo != null || photo.Length > 0)
                    {
                        var file = await FormFileExtensions.SaveAsync(photo, "UploadImages");
                        GalleryPhoto galleryPhoto = new GalleryPhoto();
                        galleryPhoto.Photo = file;
                        galleryPhoto.CreatedAt = DateTime.Now;
                        galleryPhoto.CreatedBy = _userManager.GetUserId(User);
                        galleryPhoto.IsDelete = false;
                        galleryPhoto.GalleryId = gallery.Id;
                        await _context.GalleryPhotos.AddAsync(galleryPhoto);
                        await _context.SaveChangesAsync();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(gallery);
        }

        [HttpGet("Admin/Gallery/Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gallery = await _context.Gallery.FindAsync(id);
            if (gallery == null)
            {
                return NotFound();
            }
            return View(gallery);
        }

       
        [HttpPost("Admin/Gallery/Edit")]
        public async Task<IActionResult> Edit(int id, Gallery gallery, List<IFormFile> galleryPhotos)
        {
            if (id != gallery.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    foreach (var photo in galleryPhotos)
                    {
                        if (photo != null || photo.Length > 0)
                        {
                            var file = await FormFileExtensions.SaveAsync(photo, "UploadImages");
                            GalleryPhoto galleryPhoto = new GalleryPhoto();
                            galleryPhoto.Photo = file;
                            galleryPhoto.GalleryId = id;
                            galleryPhoto.CreatedAt = DateTime.Now;
                            galleryPhoto.CreatedBy = _userManager.GetUserId(User);
                            galleryPhoto.IsDelete = false;
                            await _context.GalleryPhotos.AddAsync(galleryPhoto);
                            await _context.SaveChangesAsync();
                        }
                    }
                    gallery.UpdatedAt = DateTime.Now;
                    gallery.UpdatedBy = _userManager.GetUserId(User);
                    gallery.IsDelete = false;
                    _context.Update(gallery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GalleryExists(gallery.Id))
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
            return View(gallery);
        }

        [HttpPost("Admin/Gallery/Delete")]
        public async Task<JsonResult> OnPostDelete(int? id)
        {
            var gallery = await _context.Gallery.FindAsync(id);
            gallery.IsDelete = true;
            _context.Gallery.Update(gallery);
            await _context.SaveChangesAsync();
            Notify.Success("تمت عملية الحذف بنجاح");
            return new JsonResult(new
            {
                isValid = true,
                actionType = "redirect",
                redirectUrl = "/Admin/Gallery/Index"
            });
        }

        private bool GalleryExists(int id)
        {
            return _context.Gallery.Any(e => e.Id == id);
        }
    }
}
