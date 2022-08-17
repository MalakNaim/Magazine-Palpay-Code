using Magazine_Palpay.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Magazine_Palpay.Controllers
{
    public class HomeController : Controller
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
            var post = _context.Post
                .Include(x=>x.PostType)
                .Where(x => !x.IsDelete)
                .ToList(); 
            return View(post);
        }
    }
}
