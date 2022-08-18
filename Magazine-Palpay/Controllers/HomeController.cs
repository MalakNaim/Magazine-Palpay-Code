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
            var news = _context.LastNews.Where(x => !x.IsDelete && x.CreatedAt.Date.Equals(today)).ToList();
            ViewBag.News = news;
            var post = _context.Post.Where(x => !x.IsDelete && x.PublishedPost)
                 .Include(x=>x.PostType).OrderByDescending(x=>x.CreatedAt)
                 .ToList();
            List<Post> mainPosts = new List<Post>();
            var Deals = post.Where(x => x.PostTypeId.Equals((int)PostTypeEnum.Deals)).FirstOrDefault();
            var LastCampain = post.Where(x => x.PostTypeId.Equals((int)PostTypeEnum.Campaines)).FirstOrDefault();
            if(LastCampain != null)
            {
                ViewBag.Ads = LastCampain.MainImage;
            }
            else
            {
                ViewBag.Ads = "~/ui/images/ads.jpeg";
            }
            var IT = post.Where(x => x.PostTypeId.Equals((int)PostTypeEnum.IT)).FirstOrDefault();
            var Fintech = post.Where(x => x.PostTypeId.Equals((int)PostTypeEnum.Fintech)).FirstOrDefault();
            if(Deals != null) mainPosts.Add(Deals);
            if(LastCampain != null) mainPosts.Add(LastCampain);
            if (IT != null) mainPosts.Add(IT);
            if(Fintech != null) mainPosts.Add(Fintech); 

            ViewBag.MainPost = mainPosts;
            return View(post);
        }
    }
}
