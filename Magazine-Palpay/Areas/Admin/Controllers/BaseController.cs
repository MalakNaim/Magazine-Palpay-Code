using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Magazine_Palpay.Data;
using Magazine_Palpay.Enums;
using Magazine_Palpay.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Magazine_Palpay.Areas.Admin.Controllers
{
    public abstract class BaseController : Controller
    {
        private ApplicationDbContext _context;

        protected ApplicationDbContext Context => _context ??= HttpContext.RequestServices.GetService<ApplicationDbContext>();

        private IMapper _mapperInstance;

        protected IMapper Mapper => _mapperInstance ??= HttpContext.RequestServices.GetService<IMapper>();

        private INotyfService _notifyInstance;

        protected INotyfService Notify => _notifyInstance ??= HttpContext.RequestServices.GetService<INotyfService>();

        private IViewRenderService _viewRenderService;

        protected IViewRenderService ViewRender => _viewRenderService ??= HttpContext.RequestServices.GetService<IViewRenderService>();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var postTypes = Context.PostType.Where(x => !x.IsDelete).ToList();
            ViewBag.MultiNews = postTypes.Where(x => x.ParentId.Equals((int)PostTypeEnum.OtherNews)).ToList();
            ViewBag.Social = postTypes.Where(x => x.ParentId.Equals((int)PostTypeEnum.Social)).ToList();
            ViewBag.ListNews = Context.Post
                .Include(x => x.PostType)
                .Where(x => x.PostTypeId.Equals((int)PostTypeEnum.OtherNews)
              && !x.IsDelete && x.PublishedPost && x.OrderPlace.Equals(1) && x.MediaType.Equals(1)).Take(3)
              .OrderByDescending(x => x.CreatedAt).ToList(); 
        }

    }
}
