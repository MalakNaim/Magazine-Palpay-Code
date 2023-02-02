using Magazine_Palpay.Web;
using Magazine_Palpay.Web.Models;
using Magazine_Palpay.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Magazine_Palpay.Web.IdentityModels;

namespace Magazine_Palpay.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PostController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<FluentUser> _userManager;

        public PostController(ApplicationDbContext context, UserManager<FluentUser> userManager)
        {
            _context = context;
            _userManager = userManager;
         }

        [HttpGet("Admin/Post/Index")]
        public IActionResult Index()
        {
            var postType = _context.PostType.Where(x => !x.IsDelete && x.ParentId == 0).ToList();
            ViewBag.PostType = new SelectList(postType, "Id", "Name");
            return View();
        }

        [HttpPost("Admin/Post/LoadAll")]
        public async Task<JsonResult> LoadPostsAsync(IFormCollection form)
        {
            string draw = Request.Form["draw"].FirstOrDefault();
            string start = Request.Form["start"].FirstOrDefault();
            string length = Request.Form["length"].FirstOrDefault();
            string sortColumn = Request.Form["columns[" + Request.Form["form[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            string sortColumnDirection = Request.Form["form[0][dir]"].FirstOrDefault();
            string head = form["Head"].ToString();
            string createdAt = form["CreatedAt"].ToString();
            int.TryParse(form["OrderPlace"], out int orderPlace);
            int.TryParse(form["PostTypeId"], out int postTypeId);
            int.TryParse(form["PostSubType"], out int postSubType);
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
            var queryable = _context.Post.Where(x => !x.IsDelete)
                .OrderByDescending(x => x.Id)
                .AsQueryable();
            if (!string.IsNullOrEmpty(head))
            {
                queryable = queryable.Where(x => x.Head.Contains(head));
            }

            if(postTypeId > 0)
            {
                queryable = queryable.Where(x => x.PostTypeId.Equals(postTypeId));
            } 
            
            if(postSubType > 0)
            {
                queryable = queryable.Where(x => x.PostSubTypeId.Equals(postSubType));
            }

            if(orderPlace > 0)
            {
                queryable = queryable.Where(x => x.OrderPlace.Equals(orderPlace));
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
                    Head = x.Head,
                    CreatedAt = x.CreatedAt,
                    Published = x.PublishedPost == false ? "غير منشور" : "تم النشر",
                    PostType = GetPostType(x.PostTypeId),
                    PostSubType = x.PostSubTypeId == 0 || x.PostSubTypeId == null?"رئيسي" :GetPostType((int)x.PostSubTypeId),
                    Order = OrderPlaceName(x.OrderPlace)
                }).ToList();
               
          
           
            var jsonData = new { data = data, recordsFiltered = productList.TotalCount, recordsTotal = productList.TotalCount };
                return new JsonResult(jsonData);
            
        }

        public string OrderPlaceName(int oid)
        {
            if(oid == 1)
            {
                return "في القائمة";
            }
            else if(oid == 2)
            {
                return "في الصفحة";
            } 
            else if(oid == 3)
            {
                return "في السلايدر";
            }
            else if(oid == 4)
            {
                return "في الرئيسية col-3";
            } 
            else if(oid == 5)
            {
                return "في الرئيسية col-6";
            }
            return string.Empty;
        }

        public string GetPostType(int id)
        {
            if(id == 0 || id == null)
            {
                return string.Empty;
            }
            var postType =  _context.PostType.Find(id);
            if(postType != null)
            {
              return postType.Name;
            }
            return string.Empty;
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
            var subPost = _context.PostType.Where(x => x.ParentId.Equals(type) && !x.IsDelete).ToList();
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
            ViewBag.PostSubType = new SelectList(postSubType, "Id", "Name",post.PostSubTypeId);
            return View(post);
        }

        [HttpPost("Admin/Post/OnPostCreateOrEdit")]
        public async Task<IActionResult> OnPostCreateOrEditAsync(int id, Post post, IFormFile imgPost, List<IFormFile> postPhotos)
        {
            try {
                if (ModelState.IsValid)
                {
                    string fileName = "";
                    if (imgPost != null && imgPost.Length > 0)
                    {
                        var file = await FormFileExtensions.SaveAsync(imgPost, "UploadImages");
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
                    if (id == 0)
                    {
                        if (!string.IsNullOrEmpty(post.VideoLink))
                        {
                            post.EmbedVideoLink = GetEmbedVideoLink(post.VideoLink);
                        }
                        post.MainImage = fileName;
                        post.CreatedAt = DateTime.Now;
                        post.CreatedBy = _userManager.GetUserId(User);
                        post.IsDelete = false;
                        await _context.Post.AddAsync(post);
                        await _context.SaveChangesAsync();
                        foreach (var photo in postPhotos)
                        {
                            if (photo != null || photo.Length > 0)
                            {
                                var file = await FormFileExtensions.SaveAsync(photo, "UploadImages");
                                if (!string.IsNullOrEmpty(file))
                                {
                                    PostPhoto postPhoto = new PostPhoto();
                                    postPhoto.PostId = post.Id;
                                    postPhoto.Photo = file;
                                    postPhoto.CreatedAt = DateTime.Now;
                                    postPhoto.CreatedBy = _userManager.GetUserId(User);
                                    postPhoto.IsDelete = false;
                                    await _context.PostPhoto.AddAsync(postPhoto);
                                    await _context.SaveChangesAsync();
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
                        }

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(fileName))
                        {
                            post.MainImage = fileName;
                        }
                        foreach (var photo in postPhotos)
                        {
                            if (photo != null || photo.Length > 0)
                            {
                                var file = await FormFileExtensions.SaveAsync(photo, "UploadImages");
                                if (!string.IsNullOrEmpty(file))
                                {
                                    PostPhoto postPhoto = new PostPhoto();
                                    postPhoto.PostId = post.Id;
                                    postPhoto.Photo = file;
                                    postPhoto.CreatedAt = DateTime.Now;
                                    postPhoto.CreatedBy = _userManager.GetUserId(User);
                                    postPhoto.IsDelete = false;
                                    await _context.PostPhoto.AddAsync(postPhoto);
                                    await _context.SaveChangesAsync();
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
                        }
                        if (!string.IsNullOrEmpty(post.VideoLink))
                        {
                            post.EmbedVideoLink = GetEmbedVideoLink(post.VideoLink);
                        }
                        post.UpdatedAt = DateTime.Now;
                        post.UpdatedBy = _userManager.GetUserId(User);
                        _context.Post.Update(post);
                        await _context.SaveChangesAsync();
                    }
                }
                return new JsonResult(new
                {
                    isValid = true,
                    redirectUrl = "/Admin/Post/Index"
                });
            }
            catch(Exception ex)
            {
                Notify.Error(" فشلت عملية الإضافة، يرجى التأكد من حجم نص الخبر بأن لا يزيد عن 4000  حرف");
                return new JsonResult(new
                {
                    isValid = false
                });
            }
        }
 
        [HttpPost("Admin/Post/Delete")]
        public async Task<JsonResult> OnPostDelete(int? id)
        {
            var post = await _context.Post.FindAsync(id);
            post.IsDelete = true;
            _context.Post.Update(post);
            await _context.SaveChangesAsync();
            Notify.Success("تمت عملية الحذف بنجاح");
            return new JsonResult(new
                {
                    isValid = true,
                    actionType = "redirect",
                    redirectUrl = "/Admin/Post/Index"
                });
        }

        public string GetEmbedVideoLink(string Video)
        {
            if (Video.Substring(8, 3).Contains("www"))
            {
               var removeSub = Video.Substring(24, 8);
               var url = Video.Replace(removeSub, "embed/");
                if(url.Length > 41) { 
                   string finalUrl = url.Remove(41);
                    return finalUrl;
                }
                return url;
            }
            else
            {
                var remove = Video.Substring(8, 8);
                var url = Video.Replace(remove, "www.youtube.com/embed");
                if(url.Length > 41)
                {
                    string finalUrl = url.Remove(41);
                    return finalUrl;
                }
                return url;
            }
            return string.Empty;
        }
    }
}
