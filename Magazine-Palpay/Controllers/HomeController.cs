using Magazine_Palpay.Areas.Admin.Controllers;
using Magazine_Palpay.Data;
using Magazine_Palpay.Data.Models;
using Magazine_Palpay.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Magazine_Palpay.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var today = DateTime.Now.Date;
            var news = _context.LastNews.Where(x => !x.IsDelete).OrderByDescending(x=>x.CreatedAt).ToList();
            ViewBag.News = news;
            var post = _context.Post.Where(x => !x.IsDelete && x.PublishedPost)
                 .Include(x=>x.PostType).OrderByDescending(x=>x.CreatedAt)
                 .ToList();
            var Ads = _context.Ads.Where(x => !x.IsDelete
            && x.EndDate >= today).ToList(); 
            ViewBag.Ads = Ads; 
            ViewBag.Deals = post.Where(x => x.PostTypeId.Equals((int)PostTypeEnum.Deals) && x.MediaType.Equals(1)).FirstOrDefault();
            ViewBag.Campain = post.Where(x => x.PostTypeId.Equals((int)PostTypeEnum.Campaines) && x.MediaType.Equals(1)).FirstOrDefault();
            ViewBag.IT = post.Where(x => x.PostTypeId.Equals((int)PostTypeEnum.IT) && x.MediaType.Equals(1)).FirstOrDefault();
            ViewBag.Fintech = post.Where(x => x.PostTypeId.Equals((int)PostTypeEnum.Fintech) && x.MediaType.Equals(1)).FirstOrDefault();
            return View(post);
        }
    }
}
