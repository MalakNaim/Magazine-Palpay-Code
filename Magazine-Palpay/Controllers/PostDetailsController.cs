using Magazine_Palpay.Data;
using Magazine_Palpay.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Magazine_Palpay.Controllers
{
	public class PostDetailsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public PostDetailsController(ApplicationDbContext context)
		{
			_context = context;
		}
		[HttpGet("PostDetails/Details")]
		public IActionResult Details(int id)
		{
			var today = DateTime.Now.Date;
			var details = _context.Post.Include(x=>x.PostType).Where(x=>x.Id.Equals(id))
				.FirstOrDefault();
			var Ads = _context.Ads.Where(x => !x.IsDelete
		   && x.EndDate >= today).FirstOrDefault();
			if (Ads != null)
			{
				ViewBag.Ads = Ads.Image;
			}
			if (details.PostSubTypeId != null)
			{
				var postType = _context.PostType.Find(details.PostSubTypeId).Name;
				if(postType != null)
				ViewBag.PostSubType = postType;
			}
			else
			{
				ViewBag.PostSubType = string.Empty;
			}
			var post = _context.Post.Where(x => !x.IsDelete && x.PublishedPost)
				 .Include(x => x.PostType).OrderByDescending(x => x.CreatedAt)
				 .ToList();
			ViewBag.Posts = post.Where(x=>x.PostType.Equals((int)PostTypeEnum.OtherNews)).Take(6).ToList();
			ViewBag.MainPosts = post.Where(x=> !x.PostSubTypeId.HasValue).Take(6).ToList();
			return View(details);
		}

		[HttpGet("PostDetails/ByType")]
		public IActionResult ByType(int type)
		{
			ViewBag.PostTypeName = _context.PostType.Find(type).Name;
			var postDetails = _context.Post
				.Include(x=>x.PostType)
				.Where(x => !x.IsDelete && x.PostTypeId.Equals(type))
				.OrderByDescending(x=>x.CreatedAt).ToList();
			return View(postDetails);
		}
	}
}
