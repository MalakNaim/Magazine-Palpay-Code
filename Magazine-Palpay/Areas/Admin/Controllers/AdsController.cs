﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Magazine_Palpay.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using Magazine_Palpay.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Magazine_Palpay.Web;
using Magazine_Palpay.Web.IdentityModels;

namespace Magazine_Palpay.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AdsController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<FluentUser> _userManager;

        public AdsController(ApplicationDbContext context, UserManager<FluentUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        [HttpGet("Admin/Ads/Index")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost("Admin/Ads/LoadAll")]
        public async Task<JsonResult> LoadPostsAsync(IFormCollection form)
        {
            string draw = Request.Form["draw"].FirstOrDefault();
            string start = Request.Form["start"].FirstOrDefault();
            string length = Request.Form["length"].FirstOrDefault();
            string sortColumn = Request.Form["columns[" + Request.Form["form[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            string sortColumnDirection = Request.Form["form[0][dir]"].FirstOrDefault();
            string head = form["Head"].ToString();
            DateTime.TryParse(form["StartDate"], out DateTime startDate);
            DateTime.TryParse(form["EndDate"], out DateTime endDate);
            int page_start = int.Parse(start);
            int page_length = int.Parse(length);
            page_start = page_start / page_length;
            page_start = page_start + 1;
            var queryable = _context.Ads.Where(x => !x.IsDelete)
                .OrderByDescending(x => x.Id)
                .AsQueryable();
            if (!string.IsNullOrEmpty(head))
            {
                queryable = queryable.Where(x => x.Head.Contains(head));
            }
           
            if (startDate.Year != 0001)
            {
                queryable = queryable.Where(pc => pc.StatDate.Value.Date >= startDate);
            }

            if (endDate.Year != 0001)
            {
                queryable = queryable.Where(pc => pc.EndDate.Value.Date <= endDate);
            }

            var adsList = await queryable.ToPaginatedListAsync(page_start, page_length);
            var data = adsList.Data.Select(x => new
            {
                Id = x.Id,
                Head = x.Head,
                StartDate = x.StatDate,
                EndDate = x.EndDate,
                Owner = x.Owner,
                Order = OrderStatus(x.Order)
            }).ToList();



            var jsonData = new { data = data, recordsFiltered = adsList.TotalCount, recordsTotal = adsList.TotalCount };
            return new JsonResult(jsonData);

        }

        public string OrderStatus(int id)
        {
            switch (id)
            {
                case 1:
                    return "أعلى";
                    break;
                case 2:
                    return "أسفل";
                    break;
                case 3:
                    return "الجانب";
                    break;
                default:
                    return string.Empty;
            }
        }

        [HttpGet("Admin/Ads/Create")]
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost("Admin/Ads/Create")]
        public async Task<IActionResult> Create(Ads ads, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                string fileName = "";
                if (image != null && image.Length > 0)
                {
                    var file = await FormFileExtensions.SaveAsync(image, "UploadImages");
                    if (!string.IsNullOrEmpty(file))
                    {
                        fileName = file;
                    }
                    else
                    {
                        Notify.Error("يجب أن يكون نوع الصورة .png, .jpg, .jpeg, .gif");
                        return new JsonResult(new
                        {
                            isValid = false
                        });
                    }
                }
                ads.Image = fileName;
                ads.CreatedAt = DateTime.Now;
                ads.CreatedBy = _userManager.GetUserId(User);
                ads.IsDelete = false;
                _context.Add(ads);
                await _context.SaveChangesAsync();
                Notify.Success("تمت عملية الإضافة بنجاح"); 
                return new JsonResult(new
                {
                    isValid = true,
                    redirectUrl = "/Admin/Ads/Index"
                });
            }
            return new JsonResult(new
            {
                isValid = false
            });
        }

        [HttpGet("Admin/Ads/Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ads = await _context.Ads.FindAsync(id);
            if (ads == null)
            {
                return NotFound();
            }
            return View(ads);
        }

       
        [HttpPost("Admin/Ads/Edit")]
        public async Task<IActionResult> Edit(int id, Ads ads, IFormFile image)
        {
            if (id != ads.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var editAds = _context.Ads.Find(id);
                    string fileName = "";
                    if (image != null && image.Length > 0)
                    {
                        var file = await FormFileExtensions.SaveAsync(image, "UploadImages");
                        if (!string.IsNullOrEmpty(file))
                        {
                            fileName = file;
                        }
                        else
                        {
                            Notify.Error("يجب أن يكون نوع الصورة .png, .jpg, .jpeg, .gif");
                            return new JsonResult(new
                            {
                                isValid = false
                            });
                        }
                    }
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        editAds.Image = fileName;
                    }
                    editAds.Order = ads.Order;
                    editAds.Body = ads.Body;
                    editAds.EndDate = ads.EndDate;
                    editAds.StatDate = ads.StatDate;
                    editAds.Head = ads.Head;
                    editAds.Owner = ads.Owner;
                    editAds.Link = ads.Link;
                    editAds.UpdatedAt = DateTime.Now;
                    editAds.UpdatedBy = _userManager.GetUserId(User);
                    _context.Ads.Update(editAds);
                    await _context.SaveChangesAsync();
                    Notify.Success("تمت عملية التعديل بنجاح");
                    return new JsonResult(new
                    {
                        isValid = true,
                        redirectUrl = "/Admin/Ads/Index"
                    });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdsExists(ads.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return new JsonResult(new
            {
                isValid = true,
                redirectUrl = "/Admin/Ads/Index"
            });
        }
       
        [HttpPost("Admin/Ads/Delete")]
        public async Task<JsonResult> OnPostDelete(int? id)
        {
            var ads = await _context.Ads.FindAsync(id);
            ads.IsDelete = true;
            _context.Ads.Update(ads);
            await _context.SaveChangesAsync();
            Notify.Success("تمت عملية الحذف بنجاح");
            return new JsonResult(new
            {
                isValid = true,
                actionType = "redirect",
                redirectUrl = "/Admin/Ads/Index"
            });
        }
        private bool AdsExists(int id)
        {
            return _context.Ads.Any(e => e.Id == id);
        }
    }
}
