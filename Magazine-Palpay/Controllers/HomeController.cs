using Magazine_Palpay.Areas.Admin.Controllers;
using Magazine_Palpay.Data;
using Magazine_Palpay.Data.Models;
using Magazine_Palpay.Enums;
using Magazine_Palpay.Web.Services;
using Magazine_Palpay.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magazine_Palpay.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private IExchangeRateService exchangeService;
        private IEnumerable<SelectListItem> currencies;
 
        public HomeController(ApplicationDbContext context, IExchangeRateService exchangeService)
        {
            _context = context;
            this.exchangeService = exchangeService;
            currencies = GetCurrenciesAsync().Result ?? new List<SelectListItem>();
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
            var currency = new CurrencyViewModel()
            {
                Amount = 1,
                Rate = exchangeService.GetCurrencyRates(1, "USD", "ILS").Result,
                FromSelectedCode = "USD",
                ToSelectedCode = "ILS",
                Currencies = currencies
            };
            var model = new Tuple<List<Post>, CurrencyViewModel>(post, currency);
            return View(model);
        }


        [HttpGet("Home/Swap")]
        public JsonResult Swap(decimal amount, string from, string to, double rate)
        {
            var newModel = new CurrencyViewModel()
            {
                Amount = amount,
                Rate = 1 / rate,
                FromSelectedCode = to,
                ToSelectedCode = from,
                Currencies = currencies
            }; 
            return Json(newModel);
        }

        [HttpGet("Home/Convert")]
        public JsonResult Convert(decimal amount, string from, string to)
        {
            var newModel = new CurrencyViewModel()
            {
                Amount = amount,
                Rate = exchangeService.GetCurrencyRates(amount, from, to).Result,
                FromSelectedCode = from,
                ToSelectedCode = to,
                Currencies = currencies
            };

            return Json(newModel);
        }

        [HttpGet("Home/VideoViewer")]
        public IActionResult VideoViewer(int page = 0)
        {
            int countRow = _context.Post.Where(x => !x.IsDelete && x.MediaType.Equals(2)).Count();
            double perPage = 8;
            double NumberOfPages = Math.Ceiling(countRow / perPage);
            if (page < 1 || page > NumberOfPages)
            {
                page = 1;
            }
            int skipValue = (page - 1) * (int)perPage;
            ViewBag.NumberOfPages = NumberOfPages;
            var postLst = _context.Post
                .Where(x => !x.IsDelete && x.MediaType.Equals(2))
                .Include(x => x.PostType).OrderByDescending(x => x.CreatedAt)
                .Skip(skipValue).Take((int)perPage).ToList();
            ViewBag.page = page; 
            ViewBag.LastVideo = postLst.FirstOrDefault();
            return View(postLst);
        }

        [HttpGet("Home/MultiNews")]
        public IActionResult MultiNews(int page = 0)
        {
            int countRow = _context.Post.Where(x => !x.IsDelete && x.MediaType.Equals(1) && 
            x.PostTypeId.Equals((int)PostTypeEnum.OtherNews)).Count();
            double perPage = 6;
            double NumberOfPages = Math.Ceiling(countRow / perPage);
            if (page < 1 || page > NumberOfPages)
            {
                page = 1;
            }
            int skipValue = (page - 1) * (int)perPage;
            ViewBag.NumberOfPages = NumberOfPages;
            var news = _context.Post
                .Where(x => !x.IsDelete && x.PostTypeId.Equals((int)PostTypeEnum.OtherNews) && x.MediaType.Equals(1))
                .Include(x => x.PostType).OrderByDescending(x => x.CreatedAt)
                .Skip(skipValue).Take((int)perPage).ToList();
            ViewBag.page = page;
            return View(news);
        }

        private async Task<IEnumerable<SelectListItem>> GetCurrenciesAsync()
        {

            var currencies = new List<SelectListItem>();

            foreach (Currency currency in await exchangeService.GetCurrenciesNames())
            {
                currencies.Add(new SelectListItem
                {
                    Value = currency.Code,
                    Text = $"{currency.Code}"
                });
            }

            return new SelectList(currencies, "Value", "Text");
        }
    }
}
