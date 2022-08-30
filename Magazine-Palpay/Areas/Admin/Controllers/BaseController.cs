using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Magazine_Palpay.Web;
using Magazine_Palpay.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

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
 
    }
}
